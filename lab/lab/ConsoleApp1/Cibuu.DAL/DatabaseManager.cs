using Npgsql;
using System;

namespace Cibuu.DAL
{
    public class DatabaseManager
    {
        private string _connectionString;

        public DatabaseManager(string connectionString)
        {
            _connectionString = connectionString;
        }

        public NpgsqlConnection GetConnection()
        {
            var connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            return connection;
        }

        public void CloseConnection(NpgsqlConnection connection)
        {
            if (connection != null)
            {
                connection.Close();
            }
        }
    }
}
