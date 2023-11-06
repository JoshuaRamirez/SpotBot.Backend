using Microsoft.AspNetCore.Mvc;
using SpotBot.Server.Database.Core;
using SpotBot.Server.Services;
using SpotBot.Server.Services.Requests;
using SpotBot.Server.Services.Responses;

namespace SpotBot.WebApi.Controllers
{
    [ApiController]
    [Route("user-credentials")]
    public class UserCredentialsController : ControllerBase
    {
        [HttpPost]
        public ActionResult<PostUserCredentialsResponse> Post(PostUserCredentialsRequest postUserCredentialsRequest)
        {
            var postUserCredentialsResponse = postUserCredentialsRequest.Execute();
            if (postUserCredentialsResponse == null)
            {
                return Unauthorized();
            }
            return Ok(postUserCredentialsResponse);
        }
    }
}