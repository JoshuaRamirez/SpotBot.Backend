using SpotBot.Server.Core;
using SpotBot.Server.Database.Core;
using SpotBot.Server.Services;
using System.ComponentModel.DataAnnotations;

namespace SpotBot.Server.Services.Requests
{
    public class PatchExchangeRequest
    {
        public PatchExchangeRequest()
        {
            Id = 0;
            ApiPublicKey = "";
            ApiPrivateKey = "";
            ApiKeyPassphrase = "";
            ApiVersion = "";
        }
        [Required] public int Id { get; set; }
        [Required] public string ApiPublicKey { get; set; }
        [Required] public string ApiPrivateKey { get; set; }
        [Required] public string ApiKeyPassphrase { get; set; }
        [Required] public string ApiVersion { get; set; }

        public void Execute(int userId)
        {
            using var connection = new Connection();
            var exchangeService = new ExchangeService(connection);
            var exchangeRecord = this.ToRecord(userId);
            exchangeService.Update(exchangeRecord);
        }
    }
}
