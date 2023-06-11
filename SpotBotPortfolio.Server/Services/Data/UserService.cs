using SpotBot.Server.Database;
using SpotBot.Server.Database.Core;
using SpotBot.Server.Models.Database;

namespace SpotBot.Server.Domain
{
    public class UserService
    {
        private readonly Repository<UserRecord> _repository;

        public UserService(Connection connection)
        {
            _repository = new Repository<UserRecord>(connection);
        }

        internal UserRecord Get(int? userId)
        {
            if (userId == null)
            {
                throw new ArgumentNullException(nameof(userId));
            }
            var criteria = new UserRecord();
            criteria.Id = userId;
            var userRecord = _repository.Select(criteria).FirstOrDefault();
            if (userRecord == null)
            {
                throw new ArgumentException("Invalid User ID", nameof(userId));
            }
            return userRecord;
        }

        internal UserRecord Get(string? userName)
        {
            if (userName == null)
            {
                throw new ArgumentNullException(nameof(userName));
            }
            var criteria = new UserRecord();
            criteria.Username = userName;
            var userRecord = _repository.Select(criteria).FirstOrDefault();
            if (userRecord == null)
            {
                throw new ArgumentException("Invalid UserName", nameof(userName));
            }
            return userRecord;
        }
    }
}
