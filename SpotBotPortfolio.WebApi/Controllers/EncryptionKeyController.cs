using Microsoft.AspNetCore.Mvc;
using SpotBot.Server.Database.Core;
using SpotBot.Server.Domain;
using Microsoft.AspNetCore.Authorization;
using SpotBot.Server.Models.Resources.Responses;

namespace SpotBot.WebApi.Controllers
{
    [ApiController]
    [Route("encryption-key")]
    [Authorize]
    public class EncryptionKeyController : ControllerBase
    {

        [HttpGet("{userId}")]
        public ActionResult<GetEncryptionKeyResponse> Get(int userId)
        {
            using var connection = new Connection();
            var encryptionKeyService = new EncryptionKeyService(connection);
            var resource = encryptionKeyService.UpGet(userId);
            if (resource == null)
            {
                return NotFound();
            }
            return resource;
        }
    }
}