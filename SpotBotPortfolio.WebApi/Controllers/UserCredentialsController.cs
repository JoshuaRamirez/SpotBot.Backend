using Microsoft.AspNetCore.Mvc;
using SpotBot.Server.Database.Core;
using SpotBot.Server.Services;
using SpotBot.Server.Models.Resources.Responses;
using SpotBot.Server.Models.Resources.Requests;

namespace SpotBot.WebApi.Controllers
{
    [ApiController]
    [Route("user-credentials")]
    public class UserCredentialsController : ControllerBase
    {
        [HttpPost]
        public ActionResult<PostUserCredentialsResponse> Post(PostUserCredentialsRequest model)
        {
            using var connection = new Connection();
            var loginService = new LoginService(connection);
            var userToken = loginService.Login(model);
            if (userToken == null)
            {
                return Unauthorized();
            }
            return Ok(userToken);
        }
    }
}