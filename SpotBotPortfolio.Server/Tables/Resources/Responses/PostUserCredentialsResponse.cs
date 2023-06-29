using System.ComponentModel.DataAnnotations;

namespace SpotBot.Server.Tables.Resources.Responses
{
    public class PostUserCredentialsResponse
    {
        public int? Id { get; set; }
        public int? UserId { get; set; }
        public Guid? Token { get; set; }
        public DateTime? CreationTime { get; set; }
        public DateTime? ExpirationTime { get; set; }
        public DateTime? LastActivityTime { get; set; }
        public string? UserAgent { get; set; }
        public string? IpAddress { get; set; }
        public bool IsExpired
        {
            get
            {
                return ExpirationTime.HasValue && ExpirationTime.Value < DateTime.Now;
            }
        }
    }
}
