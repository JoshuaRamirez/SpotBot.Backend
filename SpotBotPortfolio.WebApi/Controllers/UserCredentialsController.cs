using Microsoft.AspNetCore.Mvc;
using SpotBot.Server.Database.Core;
using SpotBot.Server.Domain.Services;
using SpotBot.Server.Tables.Resources.Requests;
using SpotBot.Server.Tables.Resources.Responses;

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