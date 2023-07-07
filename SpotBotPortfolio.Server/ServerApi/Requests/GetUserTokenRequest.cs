using SpotBot.Server.Api.Responses;
using SpotBot.Server.Core;
using SpotBot.Server.Database.Core;
using SpotBot.Server.Services;

namespace SpotBot.Server.Api.Requests
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
