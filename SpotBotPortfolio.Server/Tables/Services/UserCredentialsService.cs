using SpotBot.Server.Core;
using SpotBot.Server.Database;
using SpotBot.Server.Database.Core;
using SpotBot.Server.Tables.Records;
using SpotBot.Server.Tables.Resources.Requests;

namespace SpotBot.Server.Tables.Services
{
    public class UserCredentialsService
    {
        private readonly Repository<UserRecord> _repository;

        public UserCredentialsService(Connection connection)
        {
            _repository = new Repository<UserRecord>(connection);
        }

        public bool Authenticate(PostUserCredentialsRequest resource)
        {
            var userTable = new UserRecord();
            userTable.Username = resource.Username;
            userTable = _repository.Select(userTable).FirstOrDefault();
            if (userTable == null)
            {
                return false;
            }
            if (userTable.PasswordHash == null)
            {
                throw new InvalidOperationException("Unable to retrieve user's password hash.");
            }
            if (resource.Password == null)
            {
                throw new ArgumentException("Resource is missing value for 'Password'.", "resource");
            }
            var result = Hasher.VerifyPassword(resource.Password, userTable.PasswordHash);
            return result;
        }
    }
}
