using SpotBot.Server.Core;
using SpotBot.Server.Database.Core;
using SpotBot.Server.Services;
using SpotBot.Server.Services.Responses;
using System.ComponentModel.DataAnnotations;

namespace SpotBot.Server.Services.Requests
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
