using SpotBot.Server.Database.Records.Core;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpotBot.Server.Database.Records
{
    public class EncryptionKeyRecord : ITableRecord
    {
        public int? Id { get; set; }
        public int? UserId { get; set; }
        public string? ObfuscatedValue { get; set; }
    }
}
