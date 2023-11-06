using SpotBot.Server.Core;
using SpotBot.Server.Database.Core;
using SpotBot.Server.Services;
using SpotBot.Server.Services.Responses;

namespace SpotBot.Server.Services.Requests
{
    public class GetUserTokenRequest
    {
        public Guid Token { get; set; }
        public GetUserTokenResponse? Execute()
        {
            using var connection = new Connection();
            var userTokenService = new UserTokenService(connection);
            var userTokenRecord = userTokenService.GetByToken(Token);
            var getUserTokenResponse = userTokenRecord.ToResponse();
            return getUserTokenResponse;
        }
    }
}
