using System.ComponentModel.DataAnnotations;
using SpotBot.Server.Core;
using SpotBot.Server.Database.Core;

namespace SpotBot.Server.Services.Requests;
public class PostExchangeRequest
{
    [Required] public string? ApiPublicKey { get; set; }
    [Required] public string? ApiPrivateKey { get; set; }
    [Required] public string? ApiKeyPassphrase { get; set; }
    [Required] public string? ApiVersion { get; set; }

    public void Execute(int userId)
    {
        using var connection = new Connection();
        var exchangeService = new ExchangeService(connection);
        var exchangeRecord = this.ToRecord(userId);
        exchangeService.Create(exchangeRecord);
    }
}