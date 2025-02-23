using Npgsql;
using Server.DAL.Configuration;

namespace Server.DAL.Repository
{
    public abstract class  BaseRepository
    {
        private readonly DatabaseConfig _dbConfig = new DatabaseConfig();
        public string ConnectionString => _dbConfig.ConnectionString;


        protected async Task<bool> ExecuteDataBase(NpgsqlCommand command, string sqlQuery)
        {
            await using var connection = new NpgsqlConnection(ConnectionString);

            await connection.OpenAsync();

            return (bool)(await command.ExecuteScalarAsync() ?? false);

        }
    }
}