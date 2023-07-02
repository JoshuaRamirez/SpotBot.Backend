using Microsoft.AspNetCore.Mvc;
using SpotBot.Server.Database.Core;
using Microsoft.AspNetCore.Authorization;
using SpotBot.Server.Api.Responses;
using SpotBot.Server.Api.Requests;

namespace SpotBot.WebApi.Controllers
{
    [ApiController]
    [Route("encryption-key")]
    [Authorize]
    public class EncryptionKeyController : ControllerBase
    {

        [HttpGet("{UserId}")]
        public ActionResult<GetEncryptionKeyResponse> Get([FromRoute] GetEncryptionKeyRequest getEncryptionKeyRequest)
        {
            var getEncryptionKeyResponse = getEncryptionKeyRequest.Execute();
            if (getEncryptionKeyResponse == null)
            {
                return NotFound();
            }
            return getEncryptionKeyResponse;
        }
    }
}