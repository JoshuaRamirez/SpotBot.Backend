using System.ComponentModel.DataAnnotations;

namespace SpotBot.Server.Tables.Resources.Responses
{
    public class GetExchangeResponse
    {
        public int? Id { get; set; }
        public int? UserId { get; set; }
        public string? ApiPublicKey { get; set; }
        public string? ApiPrivateKey { get; set; }
        public string? ApiKeyPassphrase { get; set; }
        public string? ApiVersion { get; set; }
    }
}
