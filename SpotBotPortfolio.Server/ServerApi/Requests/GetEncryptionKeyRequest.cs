using SpotBot.Server.Api.Responses;
using SpotBot.Server.Core;
using SpotBot.Server.Database.Core;
using SpotBot.Server.Services;

namespace SpotBot.Server.Api.Requests
{
    public class GetEncryptionKeyRequest
    {
        public int UserId { get; set; }
        public GetEncryptionKeyResponse? Execute()
        {
            using var connection = new Connection();
            var encryptionKeyService = new EncryptionKeyService(connection);
            var encriptionKeyRecord = encryptionKeyService.UpGet(UserId);
            var getEncryptionKeyResponse = encriptionKeyRecord.ToResponse();
            return getEncryptionKeyResponse;
        }
    }
}
