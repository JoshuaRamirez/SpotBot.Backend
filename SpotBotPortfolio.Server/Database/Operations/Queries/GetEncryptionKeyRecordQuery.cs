using SpotBot.Server.Database.Core;
using SpotBot.Server.Database.Operations.Core;
using SpotBot.Server.Database.Records;

namespace SpotBot.Server.Database.Operations.Queries
{
    internal class GetEncryptionKeyRecordQuery : TableQuery<EncryptionKeyRecord, EncryptionKeyRecord, EncryptionKeyRecord>
    {
        public GetEncryptionKeyRecordQuery(Connection connection) : base(connection) { }
        public override EncryptionKeyRecord? Execute(EncryptionKeyRecord criteria)
        {
            var result = _Repository.Select(criteria).FirstOrDefault();
            return result;
        }
    }
}
