using SpotBot.Server.Api.Responses;
using SpotBot.Server.Core;
using SpotBot.Server.Database.Core;
using SpotBot.Server.Services;
using System.ComponentModel.DataAnnotations;

namespace SpotBot.Server.Api.Requests
{
    public class PostUserCredentialsRequest
    {
        [Required] public string? Username { get; set; }
        [Required] public string? Password { get; set; }

        public PostUserCredentialsResponse? Execute()
        {
            using var connection = new Connection();
            var loginService = new LoginService(connection);
            var userTokenRecord = loginService.Login(Username, Password);
            var postUserCredentialsResponse = userTokenRecord.ToPostUserCredentialsResponse();
            return postUserCredentialsResponse;
        }
    }
}
