using Core.Models;
using Dapper;
using Data.Repository.Dapper;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Data.Repository.Generic
{
    public class DapperRepository<T> : IDapperRepository<T> where T : BaseEntity
    {
        private readonly IDbConnection dbConnection;

        public DapperRepository(IDbConnection connection)
        {
            this.dbConnection = connection;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await dbConnection.QueryAsync<T>("SELECT * FROM " + typeof(T).Name);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await dbConnection.QueryFirstOrDefaultAsync<T>("SELECT * FROM " + typeof(T).Name + " WHERE Id = @Id", new { Id = id });
        }

        public async Task<int> AddAsync(T entity)
        {
            return await dbConnection.ExecuteAsync("INSERT INTO " + typeof(T).Name + " VALUES (@Property1, @Property2)", entity);
        }

        public int Update(T entity)
        {
            return dbConnection.Execute("UPDATE " + typeof(T).Name + " SET Property1 = @Property1, Property2 = @Property2 WHERE Id = @Id", entity);
        }

        public int Delete(int id)
        {
            return dbConnection.Execute("DELETE FROM " + typeof(T).Name + " WHERE Id = @Id", new { Id = id });
        }
    }
}