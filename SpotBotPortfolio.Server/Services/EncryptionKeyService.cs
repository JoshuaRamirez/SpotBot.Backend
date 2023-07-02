using SpotBot.Server.Core;
using SpotBot.Server.Database.Core;
using SpotBot.Server.Database.Records;
using SpotBot.Server.Operations.Commands.Creates;
using SpotBot.Server.Operations.Queries;

namespace SpotBot.Server.Services
{
    internal class EncryptionKeyService
    {
        private readonly Connection _connection;

        public EncryptionKeyService(Connection connection)
        {
            _connection = connection;
        }

        public EncryptionKeyRecord? UpGet(int userId)
        {
            var encryptionKeyRecord = Get(userId);
            if (encryptionKeyRecord == null)
            {
                CreateFromUserId(userId);
                encryptionKeyRecord = Get(userId);
                if (encryptionKeyRecord == null)
                {
                    throw new Exception("Unable to correctly generate a new key for this user.");
                }
            }
            return encryptionKeyRecord;
        }

        public EncryptionKeyRecord? Get(int userId)
        {
            var getEncryptionKeyRecordQuery = new GetEncryptionKeyRecordQuery(_connection);
            var getEncryptionKeyRecordQueryCriteria = new EncryptionKeyRecord();
            getEncryptionKeyRecordQueryCriteria.UserId = userId;
            var encryptionKeyRecord = getEncryptionKeyRecordQuery.Execute(getEncryptionKeyRecordQueryCriteria);
            return encryptionKeyRecord;
        }

        public void CreateFromUserId(int userId)
        {
            var createEncryptionKeyRecordCommand = new CreateEncryptionKeyRecordCommand(_connection);
            var encryptionKeyRecord = new EncryptionKeyRecord();
            encryptionKeyRecord.UserId = userId;
            var key = Encryption.GenerateSymmetricKey();
            encryptionKeyRecord.ObfuscatedValue = Obfuscation.Obfuscate(key);
            createEncryptionKeyRecordCommand.Execute(encryptionKeyRecord);
        }

        
    }
}
