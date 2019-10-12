using DLearnServices.Interfaces;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DLearnAPI.Controllers
{
    public class LoginController : BaseAPIController
    {
        private readonly IUserService _userService;
        public LoginController(IUserService userService)
        {
            _userService = userService;
        }

        [Route("login/Test")]
        [HttpGet]
        public HttpResponseMessage Get()
        {
            return Request.CreateResponse(HttpStatusCode.OK, "Your token has been validated successfully: " + GetUserId());
        }

        [Route("User/GetAll")]
        [HttpGet]
        public HttpResponseMessage GetAllUsers()
        {
            return Request.CreateResponse(HttpStatusCode.OK, _userService.GetAllUsers());
        }
    }
}
