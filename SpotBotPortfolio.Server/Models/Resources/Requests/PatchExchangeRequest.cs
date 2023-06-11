using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotBot.Server.Models.Resources.Requests
{
    public class PatchExchangeRequest
    {
        public PatchExchangeRequest()
        {
            Id = 0;
            ApiPublicKey = "";
            ApiPrivateKey = "";
            ApiKeyPassphrase = "";
            ApiVersion = "";
        }
        [Required] public int Id { get; set; }
        [Required] public string ApiPublicKey { get; set; }
        [Required] public string ApiPrivateKey { get; set; }
        [Required] public string ApiKeyPassphrase { get; set; }
        [Required] public string ApiVersion { get; set; }
    }
}
