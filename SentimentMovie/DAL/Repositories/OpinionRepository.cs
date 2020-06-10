using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using Microsoft.Extensions.Configuration;
using SentimentMovie.Models;

namespace SentimentMovie.DAL
{
    public class OpinionRepository<T> : ICommonRepository<T>
        where T : OpinionDTO
    {
        private string _connectionString;

        public OpinionRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetValue<string>(Constants.MainDbConnect);
        }

        /// <summary>
        /// Получить сконфигурированное соединение
        /// </summary>
        /// <returns></returns>
        private IDbConnection GetConnection()
        {
            return (new DatabaseFactory(_connectionString)).GetDbConnection(DbType.MsSql);
        }

        public IEnumerable<T> GetAll()
        {
            using (IDbConnection db = GetConnection())
            {
                db.Open();
                return db.Query<T>("select * from dbo.tOpinion");
            }
        }

        public void Update(T data)
        {
            throw new NotImplementedException();
        }
    }
}