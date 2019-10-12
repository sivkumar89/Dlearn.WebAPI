using DLearnAPI.Filters;
using System;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Http;
using System.Web.Security;

namespace DLearnAPI.Controllers
{
    [DLearnAuthorize]
    public class BaseAPIController : ApiController
    {
        protected Guid GetUserId()
        {
            var identity = User.Identity as ClaimsIdentity;
            var userId = identity.Claims
              .Where(c => c.Type == ClaimTypes.NameIdentifier)
              .Select(c => c.Value).SingleOrDefault();

            return !string.IsNullOrEmpty(userId) ? Guid.Parse(userId) : Guid.Empty;
        }
    }
}
