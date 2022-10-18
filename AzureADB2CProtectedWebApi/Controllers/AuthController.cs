using AuthenticatioService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AzureADB2CProtectedWebApi.Controllers
{
    [Route("api/v1/authentication")]
    public class AuthController : ControllerBase
    {
        public AuthController()
        {
           
        }

        // call from angular app after login
        [HttpGet]
        [Authorize]
        [Route("get-user")]
        public IActionResult GetLoggedInUser()
        {
            var loggedInUser = UserData.GetSessionUser();
            return Ok(loggedInUser);
        }
    }
}
