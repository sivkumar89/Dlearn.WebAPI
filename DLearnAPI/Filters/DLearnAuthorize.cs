using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace DLearnAPI.Filters
{
    public class DLearnAuthorize : AuthorizeAttribute
{
    protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
    {
        actionContext.Response = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.Forbidden,
            Content = new StringContent("You are unauthorized to access this resource")
        };
    }
}
}