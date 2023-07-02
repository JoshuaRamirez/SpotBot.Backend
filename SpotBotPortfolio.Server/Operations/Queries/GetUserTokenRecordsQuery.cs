using SpotBot.Server.Database.Core;
using SpotBot.Server.Database.Records;
using SpotBot.Server.Operations.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotBot.Server.Operations.Queries
{
    internal class GetUserTokenRecordsQuery : RepositoryQuery<UserTokenRecord, UserTokenRecord, List<UserTokenRecord>>
    {
        public GetUserTokenRecordsQuery(Connection connection) : base(connection) {}
        public override List<UserTokenRecord>? Execute(UserTokenRecord? criteria = null)
        {
            // TODO: Implement get all by criteria in dapper builder
            if (criteria != null)
            {
                throw new NotImplementedException();
            }
            var userTokenRecords = _Repository.GetAll().ToList();
            return userTokenRecords;
        }
    }
}
