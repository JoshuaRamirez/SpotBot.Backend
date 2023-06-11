using System.ComponentModel.DataAnnotations;

namespace SpotBot.Server.Models.Resources.Requests
{
    public class PostUserCredentialsRequest
    {
        [Required] public string? Username { get; set; }
        [Required] public string? Password { get; set; }
    }
}
