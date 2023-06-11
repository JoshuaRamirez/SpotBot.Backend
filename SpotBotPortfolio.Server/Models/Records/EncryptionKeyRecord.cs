using SpotBot.Server.Models.Records.Core;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpotBot.Server.Models.Database
{
    internal class EncryptionKeyRecord : ITableRecord
    {
        public int? Id { get; set; }
        public int? UserId { get; set; }
        public string? ObfuscatedValue { get; set; }
    }
}
