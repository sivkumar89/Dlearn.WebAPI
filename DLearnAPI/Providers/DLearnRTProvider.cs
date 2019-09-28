using DLearnInfrastructure.Utilities;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Infrastructure;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace DLearnAPI.Providers
{
    public class DLearnRTProvider : IAuthenticationTokenProvider
    {
        private static ConcurrentDictionary<string, AuthenticationTicket> refreshTokens = new ConcurrentDictionary<string, AuthenticationTicket>();

        public async Task CreateAsync(AuthenticationTokenCreateContext context)
        {
            var guid = Guid.NewGuid().ToString();

            // maybe only create a handle the first time, then re-use for same client
            // copy properties and set the desired lifetime of refresh token
            var refreshTokenProperties = new AuthenticationProperties(context.Ticket.Properties.Dictionary)
            {
                IssuedUtc = context.Ticket.Properties.IssuedUtc,
                ExpiresUtc = DateTime.UtcNow.AddMinutes(Convert.ToInt32(Utility.GetAppSettings(DLearnConstants.TokenExpireInMinutes)))
            };
            var refreshTokenTicket = new AuthenticationTicket(context.Ticket.Identity, refreshTokenProperties);

            //_refreshTokens.TryAdd(guid, context.Ticket);
            refreshTokens.TryAdd(guid, refreshTokenTicket);

            // consider storing only the hash of the handle
            context.SetToken(guid);
        }

        public async Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {
            if (refreshTokens.TryRemove(context.Token, out AuthenticationTicket ticket))
            {
                context.SetTicket(ticket);
            }
        }

        public void Create(AuthenticationTokenCreateContext context)
        {
            throw new NotImplementedException();
        }

        public void Receive(AuthenticationTokenReceiveContext context)
        {
            throw new NotImplementedException();
        }
    }
}