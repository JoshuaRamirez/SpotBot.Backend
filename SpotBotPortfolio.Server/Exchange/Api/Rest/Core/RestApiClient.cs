using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace SpotBot.Server.Exchange.Api.Rest.Core
{
    public class RestApiClient : IDisposable
    {
        private readonly HttpClient _httpClient;
        private readonly HMACSHA256 _hmac;
        private readonly string _apiKey;
        private readonly string _passPhrase;

        public RestApiClient(string apiKey, string apiSecret, string passPhrase)
        {
            //TODO: Put the value of the uri into a config setting
            _httpClient = new HttpClient { BaseAddress = new Uri("https://api.kucoin.com") };
            _hmac = new HMACSHA256(Encoding.UTF8.GetBytes(apiSecret));
            _apiKey = apiKey;
            _passPhrase = passPhrase;
        }

        public async Task<string> PostAsync(string path)
        {
            var request = build(path, HttpMethod.Post);
            var response = await send(request);
            return response;
        }

        public async Task<string> GetAsync(string path)
        {
            var request = build(path, HttpMethod.Get);
            var response = await send(request);
            return response;
        }

        private HttpRequestMessage build(string path, HttpMethod httpMethod, bool IsSecured = true)
        {
            string httpMethodString;
            if (httpMethod == HttpMethod.Post)
            {
                httpMethodString = "POST";
            }
            else if (httpMethod == HttpMethod.Get)
            {
                 httpMethodString = "GET";
            } else
            {
                throw new ArgumentException("Unsupported HTTP Method", nameof(httpMethod));
            }
            var request = new HttpRequestMessage(httpMethod, path);
            if (IsSecured)
            {
                request = secure(request, path, httpMethodString);
            }
            return request;
        }

        private HttpRequestMessage secure(HttpRequestMessage request, string path, string httpMethodString)
        {
            var timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString();
            var signature = ComputeSignature(timestamp, httpMethodString, path, string.Empty);
            var passphraseBytes = Encoding.UTF8.GetBytes(_passPhrase);
            var encodedPassphraseBytes = _hmac.ComputeHash(passphraseBytes);
            var encodedPassphrase = Convert.ToBase64String(encodedPassphraseBytes);
            request.Headers.Add("KC-API-KEY", _apiKey);
            request.Headers.Add("KC-API-SIGN", signature);
            request.Headers.Add("KC-API-TIMESTAMP", timestamp);
            request.Headers.Add("KC-API-PASSPHRASE", encodedPassphrase);
            request.Headers.Add("KC-API-KEY-VERSION", "2");
            return request;
        }

        private async Task<string> send(HttpRequestMessage request)
        {
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