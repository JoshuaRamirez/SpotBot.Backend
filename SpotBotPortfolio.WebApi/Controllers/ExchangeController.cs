using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SpotBot.Server.Api.Requests;

namespace SpotBot.WebApi.Controllers
{
    [ApiController]
    [Route("exchange")]
    [Authorize]
    public class ExchangeController : ControllerBase
    {

        [HttpPost("{userId}")]
        public IActionResult Post(PostExchangeRequest postExchangeRequest, int userId)
        {
            postExchangeRequest.Execute(userId);
            return Ok();
        }

        [HttpPatch("{userId}")]
        public IActionResult Patch(PatchExchangeRequest patchExchangeRequest, int userId) {
            patchExchangeRequest.Execute(userId);
            return Ok();
        }

        [HttpGet("{userId}")]
        public IActionResult Get([FromRoute] GetExchangeRequest getExchangeRequest)
        {
            var getExchangeResponse = getExchangeRequest.Execute();
            if (getExchangeResponse == null)
            {
                return NotFound();
            }
            return Ok(getExchangeResponse);
        } 

    }
}