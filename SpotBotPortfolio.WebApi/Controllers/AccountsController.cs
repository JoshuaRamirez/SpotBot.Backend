using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SpotBot.Server.Api.Requests;
using SpotBot.Server.Api.Responses;

namespace SpotBot.WebApi.Controllers
{
    [ApiController]
    [Route("accounts")]
    [Authorize]
    public class AccountsController : ControllerBase
    {

        [HttpGet("{UserId}")]
        public ActionResult<GetAccountsResponse> Get([FromRoute] GetAccountsRequest getAccountsRequest)
        {
            var getAccountsResponse = getAccountsRequest.Execute();
            if (getAccountsResponse == null)
            {
                return NotFound();
            }
            return getAccountsResponse;
        }
    }
}