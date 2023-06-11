using System.ComponentModel.DataAnnotations;

namespace SpotBot.Server.Models.Resources.Requests
{
    public class PostExchangeRequest
    {
        public PostExchangeRequest()
        {
            ApiPublicKey = "";
            ApiPrivateKey = "";
            ApiKeyPassphrase = "";
            ApiVersion = "";
        }
        [Required] public string ApiPublicKey { get; set; }
        [Required] public string ApiPrivateKey { get; set; }
        [Required] public string ApiKeyPassphrase { get; set; }
        [Required] public string ApiVersion { get; set; }
    }
}
