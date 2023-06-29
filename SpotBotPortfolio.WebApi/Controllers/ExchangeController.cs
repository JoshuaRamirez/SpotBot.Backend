using Microsoft.AspNetCore.Mvc;
using SpotBot.Server.Database.Core;
using Microsoft.AspNetCore.Authorization;
using SpotBot.Server.Tables.Resources.Requests;
using SpotBot.Server.Tables.Services;

namespace SpotBot.WebApi.Controllers
{
    [ApiController]
    [Route("exchange")]
    [Authorize]
    public class ExchangeController : ControllerBase
    {

        [HttpPost("{userId}")]
        public IActionResult Post(PostExchangeRequest resource, int userId)
        {
            using var connection = new Connection();
            var exchangeService = new ExchangeService(connection);
            exchangeService.Create(userId, resource);
            return Ok();
        }

        [HttpPatch("{userId}")]
        public IActionResult Patch(PatchExchangeRequest resource, int userId) {
            using var connection = new Connection();
            var exchangeService = new ExchangeService(connection);
            exchangeService.Update(userId, resource);
            return Ok();
        }

        [HttpGet("{userId}")]
        public IActionResult Get(int userId)
        {
            using var connection = new Connection();
            var exchangeService = new ExchangeService(connection);
            var exchange = exchangeService.Get(userId);
            if (exchange == null)
            {
                return NotFound();
            }
            return Ok(exchange);
        } 

    }
}