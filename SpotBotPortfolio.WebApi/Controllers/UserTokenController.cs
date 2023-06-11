using Microsoft.AspNetCore.Mvc;
using SpotBot.Server.Database.Core;
using SpotBot.Server.Domain;

namespace SpotBot.WebApi.Controllers
{
    [ApiController]
    [Route("user-token")]
    public class UserTokenController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get(Guid token)
        {
            using var connection = new Connection();
            var userTokenService = new UserTokenService(connection);
            var userToken = userTokenService.Get(token);
            if (userToken == null)
            {
                return NotFound();
            }
            return Ok(userToken);
        }
    }
}
