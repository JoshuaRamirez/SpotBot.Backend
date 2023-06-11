using SpotBot.Server.Core;
using SpotBot.Server.Database;
using SpotBot.Server.Database.Core;
using SpotBot.Server.Models.Database;
using SpotBot.Server.Models.Resources.Responses;

namespace SpotBot.Server.Domain
{
    public class EncryptionKeyService
    {
        private Repository<EncryptionKeyRecord> _encryptionKeyRepository;

        public EncryptionKeyService(Connection connection)
        {
            _encryptionKeyRepository = new Repository<EncryptionKeyRecord>(connection);
        }

        public GetEncryptionKeyResponse? UpGet(int userId)
        {
            var encryptionKey = this.Get(userId);
            if (encryptionKey == null)
            {
                this.Create(userId);
                encryptionKey = this.Get(userId);
                if (encryptionKey == null)
                {
                    throw new Exception("Unable to correctly generate a new key for this user.");
                }
            }
            return encryptionKey;
        }

        public GetEncryptionKeyResponse? Get(int userId)
        {
            var criteria = new EncryptionKeyRecord();
            criteria.UserId = userId;
            var results = _encryptionKeyRepository.Select(criteria);
            var encryptionKeyTable = results.FirstOrDefault();
            if (encryptionKeyTable == null)
            {
                return null;
            }

            var encryptionKey = new GetEncryptionKeyResponse();
            encryptionKey.Id = userId;
            encryptionKey.UserId = userId;
            encryptionKey.Value = encryptionKeyTable.ObfuscatedValue;
            return encryptionKey;
        }
        public void Create(int userId)
        {
            var model = new EncryptionKeyRecord();
            model.UserId = userId;
            var key = Encryption.GenerateSymmetricKey();
            model.ObfuscatedValue = Obfuscation.Obfuscate(key);
            _encryptionKeyRepository.Insert(model);
        }
    }
}
