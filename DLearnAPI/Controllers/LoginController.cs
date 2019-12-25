using DLearnServices.Entities;
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

        [Route("GetStates")]
        [HttpGet]
        public HttpResponseMessage GetAllStates()
        {
            return Request.CreateResponse(HttpStatusCode.OK, _userService.GetAllStates());
        }

        [Route("User/AddAddress")]
        [HttpPost]
        public HttpResponseMessage AddUserAddress(AddressEntity addressEntity)
        {
            if (addressEntity != null)
            {
                addressEntity.UserId = GetUserId();
                return Request.CreateResponse(HttpStatusCode.OK, _userService.AddUserAddress(addressEntity));
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }
    }
}
