using SpotBot.Server.Core;
using SpotBot.Server.Database.Core;
using SpotBot.Server.Tables.Services;

namespace SpotBot.Server.Exchange
{
    public class ExchangeClientFactory
    {
        private readonly Connection _connection;

        public ExchangeClientFactory(Connection connection)
        {
            if (connection == null)
            {
                throw new ArgumentNullException(nameof(connection));
            }
            _connection = connection;

        }
        public ExchangeClient Create(int userId)
        {
            var encryptionKeyService = new EncryptionKeyService(_connection);
            var getEncryptionKeyResponse = encryptionKeyService.Get(userId);
            if (getEncryptionKeyResponse == null)
            {
                throw new InvalidOperationException("EncryptionKey response is null.");
            }
            if (string.IsNullOrEmpty(getEncryptionKeyResponse.Value))
            {
                throw new InvalidOperationException("Encryption key is null or empty.");
            }
            var obfuscatedEncryptionKey = getEncryptionKeyResponse.Value;
            var encryptionKey = Obfuscation.Deobfuscate(obfuscatedEncryptionKey);
            var exchangeService = new ExchangeService(_connection);
            var getExchangeResponse = exchangeService.Get(userId);
            if (getExchangeResponse == null)
            {
                throw new InvalidOperationException("Exchange response is null.");
            }

            var apiKey = getExchangeResponse.ApiPublicKey;
            if (string.IsNullOrEmpty(apiKey))
            {
                throw new InvalidOperationException("API key is null or empty.");
            }

            var apiSecret = getExchangeResponse.ApiPrivateKey;
            if (string.IsNullOrEmpty(apiSecret))
            {
                throw new InvalidOperationException("API secret is null or empty.");
            }

            var passPhrase = getExchangeResponse.ApiKeyPassphrase;
            if (string.IsNullOrEmpty(passPhrase))
            {
                throw new InvalidOperationException("API key passphrase is null or empty.");
            }
            var encryption = new Encryption(encryptionKey);
            apiKey = encryption.Decrypt(apiKey);
            apiSecret = encryption.Decrypt(apiSecret);
            passPhrase = encryption.Decrypt(passPhrase);

            var exchangeClient = new ExchangeClient(apiKey, apiSecret, passPhrase);
            return exchangeClient;
        }
    }
}
