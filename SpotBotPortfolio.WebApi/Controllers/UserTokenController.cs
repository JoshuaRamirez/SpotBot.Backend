using Microsoft.AspNetCore.Mvc;
using SpotBot.Server.Services.Requests;

namespace SpotBot.WebApi.Controllers
{
    [ApiController]
    [Route("user-token")]
    public class UserTokenController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get([FromQuery] GetUserTokenRequest getUserTokenRequest)
        {
            var getUserTokenResponse = getUserTokenRequest.Execute();
            if (getUserTokenResponse == null)
            {
                return NotFound();
            }
            return Ok(getUserTokenResponse);
        }
    }
}
