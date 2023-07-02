using System.ComponentModel.DataAnnotations;

namespace SpotBot.Server.Api.Responses
{
    public class GetEncryptionKeyResponse
    {
        public int? Id { get; set; }
        public int? UserId { get; set; }
        public string? Value { get; set; }
    }
}
