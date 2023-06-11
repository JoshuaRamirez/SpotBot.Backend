using Microsoft.AspNetCore.Mvc;
using SpotBot.Server.Database.Core;
using SpotBot.Server.Domain;
using Microsoft.AspNetCore.Authorization;
using SpotBot.Server.Models.Resources.Responses;
using SpotBot.Server.Models.Resources.Requests;
using SpotBot.Server.Services.Exchange;

namespace SpotBot.WebApi.Controllers
{
    [ApiController]
    [Route("accounts")]
    [Authorize]
    public class AccountsController : ControllerBase
    {

        [HttpGet("{userId}")]
        public ActionResult<GetAccountsResponse> Get(int userId)
        {
            using var connection = new Connection();
            var accountsService = new AccountsService(connection);
            var resource = accountsService.Get(userId);
            if (resource == null)
            {
                return NotFound();
            }
            return resource;
        }
    }
}