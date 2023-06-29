using Dapper;
using MySqlConnector;
using SpotBot.Server.Database.Core;
using SpotBot.Server.Tables.Records.Core;

namespace SpotBot.Server.Database
{
    internal class Repository<TModel> where TModel : ITableRecord
    {
        private readonly Connection _connection;
        private readonly MySqlConnection? _mySqlConnection;

        public Repository(Connection connection)
        {
            _connection = connection;
            _mySqlConnection = _connection.MySqlConnection;
        }

        private int? Upsert(DapperSqlBuilder<TModel> builder, TModel model)
        {
            var idQuery = builder.BuildIdQuery();
            var existingId = _mySqlConnection.ExecuteScalar<int?>(idQuery, model);
            int? id = null;
            if (existingId.HasValue)
            {
                model.Id = existingId;
                var updateQuery = builder.BuildUpdateQuery();
                _mySqlConnection.Execute(updateQuery, model);
            }
            else
            {
                var insertQuery = builder.BuildInsertQuery();
                id = _mySqlConnection.ExecuteScalar<int>(insertQuery, model);
            }
            return id;
        }

        internal IList<TModel> Select(TModel criteria)
        {
            var builder = new DapperSqlBuilder<TModel>(criteria);
            var selectQuery = builder.BuildSelectQuery();
            var results = _mySqlConnection.Query<TModel>(selectQuery, criteria);
            return results.ToList();
        }

        internal IList<TModel> GetAll()
        {
            var selectAllQuery = DapperSqlBuilder<TModel>.BuildSelectAllQuery();
            var results = _mySqlConnection.Query<TModel>(selectAllQuery);
            return results.ToList();
        }

        internal void Update(TModel record)
        {
            var builder = new DapperSqlBuilder<TModel>(record);
            Upsert(builder, record);
        }

        internal int Insert(TModel criteria)
        {
            var builder = new DapperSqlBuilder<TModel>(criteria);
            var insertQuery = builder.BuildInsertQuery();
            var newId = _mySqlConnection.ExecuteScalar<int>(insertQuery, criteria);
            return newId;
        }

        internal void Delete(TModel criteria)
        {
            var builder = new DapperSqlBuilder<TModel>(criteria);
            var deleteQuery = builder.BuildDeleteQuery();
            _mySqlConnection.Execute(deleteQuery, criteria);
        }

        
    }

}