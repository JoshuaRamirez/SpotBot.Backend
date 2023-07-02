using SpotBot.Server.Database.Core;
using SpotBot.Server.Database.Records;
using SpotBot.Server.Operations.Core;

namespace SpotBot.Server.Operations.Queries
{
    internal class GetEncryptionKeyRecordQuery : RepositoryQuery<EncryptionKeyRecord, EncryptionKeyRecord, EncryptionKeyRecord>
    {
        public GetEncryptionKeyRecordQuery(Connection connection) : base(connection) {}
        public override EncryptionKeyRecord? Execute(EncryptionKeyRecord criteria)
        {
            var result = _Repository.Select(criteria).FirstOrDefault();
            return result;
        }
    }
}
