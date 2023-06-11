using System.ComponentModel.DataAnnotations;

namespace SpotBot.Server.Models.Resources.Responses
{
    public class GetEncryptionKeyResponse
    {
        public int? Id { get; set; }
        public int? UserId { get; set; }
        public string? Value { get; set; }
    }
}
