using SpotBot.Server.Tables.Records.Core;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpotBot.Server.Tables.Records
{
    internal class EncryptionKeyRecord : ITableRecord
    {
        public int? Id { get; set; }
        public int? UserId { get; set; }
        public string? ObfuscatedValue { get; set; }
    }
}
