using System;
using System.Data;
using System.Data.SqlClient;

namespace SentimentMovie.DAL
{
    public class DatabaseFactory
    {
        private string _connectStr;

        public DatabaseFactory(string connectStr)
        {
            _connectStr = connectStr;
        }

        public IDbConnection GetDbConnection(DbType dbType)
        {
            switch (dbType)
            {
                case DbType.MsSql:
                    return new SqlConnection(_connectStr);
                default:
                    throw new NotSupportedException("Данная ветка кода не поддерживается!");
            }
        }

    }
}
