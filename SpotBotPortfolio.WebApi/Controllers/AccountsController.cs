using Microsoft.AspNetCore.Mvc;
using SpotBot.Server.Database.Core;
using Microsoft.AspNetCore.Authorization;
using SpotBot.Server.Exchange.RestApi.Resources.Gets.Responses;
using SpotBot.Server.Exchange.Services;

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
            var accountsService = new AccountService(connection);
            var resource = accountsService.Get(userId);
            if (resource == null)
            {
                return NotFound();
            }
            return resource;
        }
    }
}