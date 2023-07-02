using SpotBot.Server.Database.Records.Core;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpotBot.Server.Database.Records
{
    internal class UserRecord : ITableRecord
    {
        public int? Id { get; set; }
        public string? Username { get; set; }
        public string? PasswordHash { get; set; }
        public string? Email { get; set; }
    }
}
