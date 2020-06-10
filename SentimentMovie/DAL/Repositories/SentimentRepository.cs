using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using Microsoft.Extensions.Configuration;
using SentimentMovie.Models;

namespace SentimentMovie.DAL.Repositories
{
    public class SentimentRepository<T> : ICommonRepository<T>
        where T : SentimentDTO
    {
        private string _connectionString;

        public SentimentRepository(IConfiguration configuration)
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
                return db.Query<T>("select * from dbo.tSentiment");
            }
        }

        public void Update(T data)
        {
            using (IDbConnection db = GetConnection())
            {
                db.Open();
                string updateQuery = @"UPDATE dbo.tSentiment SET Name = @Name WHERE Id = @Id";

                db.Execute(updateQuery, new
                {
                    data.Name,
                    data.Id
                });
            }
        }
    }
}
