using System.ComponentModel.DataAnnotations;

namespace SpotBot.Server.Services.Responses
{
    public class GetEncryptionKeyResponse
    {
        public int? Id { get; set; }
        public int? UserId { get; set; }
        public string? Value { get; set; }
    }
}
