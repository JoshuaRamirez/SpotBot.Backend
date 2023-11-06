using SpotBot.Server.Database.Records.Core;
using System.Dynamic;
using System.Linq.Expressions;

namespace SpotBot.Server.Database.Core
{
    internal class DapperSqlBuilder<TModel> where TModel : ITableRecord
    {
        private readonly TModel _model;
        private readonly string _tableName;
        private readonly string _primaryKey;
        private readonly List<string> _fields;

        public DapperSqlBuilder(TModel model)
        {
            _model = model;
            _tableName = typeof(TModel).Name.Replace("Record", "");
            _primaryKey = "Id";
            _fields = typeof(TModel).GetProperties().Select(prop => prop.Name).ToList();
        }

        public string BuildUpdateQuery()
        {
            var setFields = getFieldNames().Where(x => x != "Id");
            var setClauses = setFields.Select(field => $"{field} = @{field}");
            return $"UPDATE {_tableName} SET {string.Join(", ", setClauses)} WHERE {_primaryKey} = @{_primaryKey}";
        }

        public string BuildUpdateQuery(Expression<Func<TModel, int>> foreignKeyExpression)
        {
            var fields = getFieldNames();
            var foreignKeyPropertyName = getForeignKeyPropertyName(foreignKeyExpression);
            var joinClause = getJoinClause(foreignKeyPropertyName);
            var foreignKeyValue = getForeignKeyValue(foreignKeyExpression);
            var setClauses = fields.Select(field => $"{_tableName}.{field} = @{field}");
            return $"UPDATE {_tableName} {joinClause} SET {string.Join(", ", setClauses)} WHERE {_tableName}.{_primaryKey} = @{_tableName}.{_primaryKey} AND {_tableName}.{foreignKeyPropertyName} = {foreignKeyValue}";
        }

        public string BuildInsertQuery()
        {
            var fieldNames = getFieldNames();
            var fieldValueTokens = fieldNames.Select(field => $"@{field}");
            return $"INSERT INTO {_tableName} ({string.Join(", ", fieldNames)}) VALUES ({string.Join(", ", fieldValueTokens)}); SELECT LAST_INSERT_ID();";
        }
        public string BuildDeleteQuery()
        {
            var whereClauses = getFieldNames().Select(field => $"{field} = @{field}");
            return $"DELETE FROM {_tableName} WHERE {string.Join(" AND ", whereClauses)}";
        }

        public string BuildInsertQuery(Expression<Func<TModel, int>> foreignKeyExpression)
        {
            var fieldNames = getFieldNames();
            var foreignKeyPropertyName = getForeignKeyPropertyName(foreignKeyExpression);
            var joinClause = getJoinClause(foreignKeyPropertyName);
            var foreignKeyValue = getForeignKeyValue(foreignKeyExpression);
            var fieldValueTokens = fieldNames.Select(field => $"@'{field}'");
            var fieldsWithForeignKey = new List<string>(fieldNames) { foreignKeyPropertyName };
            var fieldValuesWithForeignKey = new List<string>(fieldValueTokens) { foreignKeyValue.ToString() };

            return $"INSERT INTO {_tableName} {joinClause} ({string.Join(", ", fieldsWithForeignKey)}) VALUES ({string.Join(", ", fieldValuesWithForeignKey)})";
        }

        public string BuildIdQuery()
        {
            return $"SELECT {_primaryKey} FROM {_tableName} WHERE {_primaryKey} = @{_primaryKey}";
        }

        public string BuildSelectQuery()
        {
            var fieldNames = getFieldNames();
            var whereClauses = fieldNames.Select(field => $"{field} = @{field}");
            return $"SELECT {string.Join(", ", _fields)} FROM {_tableName} WHERE {string.Join(" AND ", whereClauses)}";
        }

        public string BuildExistsQuery()
        {
            var fieldNames = getFieldNames();
            var hasIdInCriteria = fieldNames.Contains("Id");
            if (hasIdInCriteria )
            {
                return BuildIdQuery();
            } else
            {
                var whereClauses = fieldNames.Select(field => $"{field} = @{field}");
                return $"SELECT {_primaryKey} FROM {_tableName} WHERE {string.Join(" AND ", whereClauses)}";
            }
        }

        public static string BuildSelectAllQuery()
        {
            var tableName = typeof(TModel).Name.Replace("Table", "");
            return $"SELECT * FROM {tableName}";
        }

        public object BuildParameters(TModel model)
        {
            var fieldNames = getFieldNames();
            var param = new ExpandoObject() as IDictionary<string, Object>;
            foreach (var field in fieldNames)
            {
                var property = typeof(TModel).GetProperty(field);
                if (property != null)
                {
                    var value = property.GetValue(model);
                    if (value != null)
                    {
                        param.Add(field, value);
                    }
                }
            }
            return param;
        }

        private IEnumerable<string> getFieldNames()
        {
            return _fields.Where(field => GetPropertyValue(_model, field) != null).ToList();
        }

        private object? GetPropertyValue(TModel model, string propertyName)
        {
            var property = typeof(TModel).GetProperty(propertyName);
            if (property == null)
            {
                throw new ArgumentException("Model doesn't contain the expected property.", "propertyName");
            }
            return property.GetValue(model);
        }

        private string getForeignKeyPropertyName(Expression<Func<TModel, int>> foreignKeyExpression)
        {
            if (!(foreignKeyExpression.Body is MemberExpression memberExpression))
                throw new ArgumentException("Expression must be a member expression");

            if (memberExpression.Member.DeclaringType != typeof(TModel))
                throw new ArgumentException("Member must be declared in TModel");

            var propertyName = memberExpression.Member.Name;
            var foreignKeySuffix = "Id";

            if (!propertyName.EndsWith(foreignKeySuffix))
                throw new ArgumentException($"Member {propertyName} must end with '{foreignKeySuffix}' to be a foreign key property");

            return propertyName;
        }


        private string getJoinClause(string foreignKeyPropertyName)
        {
            var foreignTableName = foreignKeyPropertyName.Substring(0, foreignKeyPropertyName.Length - 2);
            return $"INNER JOIN {foreignTableName} ON {_tableName}.{foreignKeyPropertyName} = {foreignTableName}.Id";
        }

        private int getForeignKeyValue(Expression<Func<TModel, int>> foreignKeyExpression)
        {
            var foreignKeyPropertyName = getForeignKeyPropertyName(foreignKeyExpression);
            var foreignKeyProperty = typeof(TModel).GetProperty(foreignKeyPropertyName);
            if (foreignKeyProperty == null)
            {
                throw new InvalidOperationException("Unknown Error");
            }
            var foreignKeyPropertyValue = foreignKeyProperty.GetValue(_model);
            if (foreignKeyPropertyValue == null)
            {
                throw new InvalidOperationException("Unknown Error");
            }
            return (int)foreignKeyPropertyValue;
        }
    }

}
