using System.Text;
using System.Security.Cryptography;

namespace SpotBot.Server.Exchange
{
    public class ExchangeClient : IDisposable
    {
        private readonly HttpClient _httpClient;
        private readonly HMACSHA256 _hmac;
        private readonly string _apiKey;
        private readonly string _passPhrase;

        public ExchangeClient(string apiKey, string apiSecret, string passPhrase)
        {
            _httpClient = new HttpClient { BaseAddress = new Uri("https://api.kucoin.com") };
            _hmac = new HMACSHA256(Encoding.UTF8.GetBytes(apiSecret));
            _apiKey = apiKey;
            _passPhrase = passPhrase;
        }

        public async Task<string> GetAsync(string path)
        {
            var timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString();
            var signature = ComputeSignature(timestamp, "GET", path, string.Empty);
            var passphraseBytes = Encoding.UTF8.GetBytes(_passPhrase);
            var encodedPassphraseBytes = _hmac.ComputeHash(passphraseBytes);
            var encodedPassphrase = Convert.ToBase64String(encodedPassphraseBytes);
            var request = new HttpRequestMessage(HttpMethod.Get, path);
            request.Headers.Add("KC-API-KEY", _apiKey);
            request.Headers.Add("KC-API-SIGN", signature);
            request.Headers.Add("KC-API-TIMESTAMP", timestamp);
            request.Headers.Add("KC-API-PASSPHRASE", encodedPassphrase);
            request.Headers.Add("KC-API-KEY-VERSION", "2");

            var response = _httpClient.Send(request);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }

        private string ComputeSignature(string timestamp, string method, string path, string body)
        {
            var prehash = timestamp + method + path + body;
            var hash = _hmac.ComputeHash(Encoding.UTF8.GetBytes(prehash));
            return Convert.ToBase64String(hash);
        }

        public void Dispose()
        {
            _httpClient.Dispose();
            _hmac.Dispose();
        }
    }
}