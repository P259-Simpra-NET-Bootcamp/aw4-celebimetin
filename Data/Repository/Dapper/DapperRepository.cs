using Core.Models;
using Dapper;
using Data.Repository.Dapper;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.Generic
{
    public class DapperRepository<T> : IDapperRepository<T> where T : BaseEntity
    {
        private readonly IDbConnection connection;

        private IEnumerable<PropertyInfo> GetProperties => typeof(T).GetProperties();

        public DapperRepository(IDbConnection connection)
        {
            this.connection = connection;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await connection.QueryAsync<T>("SELECT * FROM " + typeof(T).Name);

        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await connection.QueryFirstOrDefaultAsync<T>("SELECT * FROM " + typeof(T).Name + " WHERE Id = @Id", new { Id = id });
        }

        public async Task<int> AddAsync(T entity)
        {
            var insertQuery = GenerateInsertQuery();
            return await connection.ExecuteAsync(insertQuery, entity);
        }

        public int Update(T entity)
        {
            var updateQuery = GenerateUpdateQuery();
            return connection.Execute(updateQuery, entity);
        }

        public int Delete(int id)
        {
            return connection.Execute("DELETE FROM " + typeof(T).Name + " WHERE Id = @Id", new { Id = id });
        }

        public async Task<int> SaveRangeAsync(IEnumerable<T> list)
        {
            var inserted = 0;
            var query = GenerateInsertQuery();

            inserted += await connection.ExecuteAsync(query, list);

            return inserted;
        }

        private static List<string> GenerateListOfProperties(IEnumerable<PropertyInfo> listOfProperties)
        {
            return (from prop in listOfProperties
                    let attributes = prop.GetCustomAttributes(typeof(DescriptionAttribute), false)
                    where attributes.Length <= 0 || (attributes[0] as DescriptionAttribute)?.Description != "ignore"
                    select prop.Name).ToList();
        }

        private string GenerateInsertQuery()
        {
            var insertQuery = new StringBuilder("INSERT INTO " + typeof(T).Name);

            insertQuery.Append("(");

            var properties = GenerateListOfProperties(GetProperties);
            properties.ForEach(prop => { insertQuery.Append($"[{prop}],"); });

            insertQuery
                .Remove(insertQuery.Length - 1, 1)
                .Append(") VALUES (");

            properties.ForEach(prop => { insertQuery.Append($"@{prop},"); });

            insertQuery
                .Remove(insertQuery.Length - 1, 1)
                .Append(")");

            return insertQuery.ToString();
        }

        private string GenerateUpdateQuery()
        {
            var updateQuery = new StringBuilder("UPDATE " + typeof(T).Name + "SET ");
            var properties = GenerateListOfProperties(GetProperties);

            properties.ForEach(property =>
            {
                if (!property.Equals("Id"))
                {
                    updateQuery.Append($"{property}=@{property},");
                }
            });

            updateQuery.Remove(updateQuery.Length - 1, 1);
            updateQuery.Append(" WHERE Id=@Id");

            return updateQuery.ToString();
        }
    }
}