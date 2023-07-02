using SpotBot.Server.Core;
using SpotBot.Server.Database.Core;
using SpotBot.Server.Services;

namespace SpotBot.Server.Exchange.RestApi.Core
{
    public class RestApiClientFactory
    {
        private readonly Connection _connection;

        public RestApiClientFactory(Connection connection)
        {
            if (connection == null)
            {
                throw new ArgumentNullException(nameof(connection));
            }
            _connection = connection;

        }
        public RestApiClient Create(int userId)
        {
            var encryptionKeyService = new EncryptionKeyService(_connection);
            var encryptionKeyRecord = encryptionKeyService.Get(userId);
            if (encryptionKeyRecord == null)
            {
                throw new InvalidOperationException("EncryptionKey response is null.");
            }
            if (encryptionKeyRecord.ObfuscatedValue == null)
            {
                throw new InvalidOperationException("EncryptionKey text value is null.");
            }
            var encryptionKeyValue = Obfuscation.Deobfuscate(encryptionKeyRecord.ObfuscatedValue);
            var exchangeService = new ExchangeService(_connection);
            var exchangeRecord = exchangeService.GetByUserId(userId);
            if (exchangeRecord == null)
            {
                throw new InvalidOperationException("Exchange response is null.");
            }

            var apiKey = exchangeRecord.ApiPublicKey;
            if (string.IsNullOrEmpty(apiKey))
            {
                throw new InvalidOperationException("API key is null or empty.");
            }

            var apiSecret = exchangeRecord.ApiPrivateKey;
            if (string.IsNullOrEmpty(apiSecret))
            {
                throw new InvalidOperationException("API secret is null or empty.");
            }

            var passPhrase = exchangeRecord.ApiKeyPassphrase;
            if (string.IsNullOrEmpty(passPhrase))
            {
                throw new InvalidOperationException("API key passphrase is null or empty.");
            }
            var encryption = new Encryption(encryptionKeyValue);
            apiKey = encryption.Decrypt(apiKey);
            apiSecret = encryption.Decrypt(apiSecret);
            passPhrase = encryption.Decrypt(passPhrase);
            var restApiClient = new RestApiClient(apiKey, apiSecret, passPhrase);
            return restApiClient;
        }
    }
}
