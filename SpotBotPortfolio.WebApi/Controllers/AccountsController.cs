using Microsoft.AspNetCore.Mvc;
using SpotBot.Server.Database.Core;
using Microsoft.AspNetCore.Authorization;
using SpotBot.Server.Exchange.RestApi.Services;
using SpotBot.Server.Exchange.RestApi.Responses;

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
            var accountsService = new GetAccountsRequest(connection);
            var resource = accountsService.Execute(userId);
            if (resource == null)
            {
                return NotFound();
            }
            return resource;
        }
    }
}