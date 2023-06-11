using SpotBot.Server.Models.Records.Core;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpotBot.Server.Models.Database
{
    internal class UserRecord : ITableRecord
    {
        public int? Id { get; set; }
        public string? Username {get; set;}
        public string? PasswordHash {get; set;}
        public string? Email {get; set;}
    }
}
