using SpotBot.Server.Tables.Records.Core;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpotBot.Server.Tables.Records
{
    internal class ExchangeRecord : ITableRecord
    {
        public int? Id { get; set; }
        public int? UserId { get; set; }
        public string? ApiPublicKey { get; set; }
        public string? ApiPrivateKey { get; set; }
        public string? ApiKeyPassphrase { get; set; }
        public string? ApiVersion { get; set; }
    }
}
