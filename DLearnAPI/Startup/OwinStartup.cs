using System;
using System.Web.Http;
using DLearnAPI.Providers;
using DLearnInfrastructure.Utilities;
using DLearnServices.Interfaces;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security.OAuth;
using Owin;

[assembly: OwinStartup(typeof(DLearnAPI.Startup.OwinStartup))]

namespace DLearnAPI.Startup
{
    public class OwinStartup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            
            app.Use<DLearnAuthMiddleware>();

            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
            ConfigureAuth(app);

            WebApiConfig.Register(config);
            
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);
        }

        public void ConfigureAuth(IAppBuilder app)
        {
            byte[] symmetricKey = Convert.FromBase64String(DLearnConstants.SecretKey);

            var dLearnOAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/login"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(Convert.ToInt32(DLearnInfrastructure.Utilities.Utility.GetAppSettings(DLearnConstants.TokenExpireInMinutes))),
                AccessTokenFormat = new DLearnJWTFormat(),
                Provider = new DLearnOAuthProvider((IUserService)GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(IUserService))),
                RefreshTokenProvider = new DLearnRTProvider(),
                AllowInsecureHttp = true
            };

            var dLearnJWTOptions = new JwtBearerAuthenticationOptions
            {
                TokenValidationParameters = new TokenValidationParameters()
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(symmetricKey),
                    ClockSkew = TimeSpan.FromMinutes(0)
                },
            };

            app.UseOAuthAuthorizationServer(dLearnOAuthOptions);
            app.UseJwtBearerAuthentication(dLearnJWTOptions);
        }
    }
}