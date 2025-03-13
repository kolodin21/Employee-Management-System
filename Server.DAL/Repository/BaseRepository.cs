using Npgsql;
using Server.DAL.Configuration;

namespace Server.DAL.Repository
{
    public abstract class  BaseRepository
    {
        private readonly DatabaseConfig _dbConfig = new DatabaseConfig();
        public string ConnectionString => _dbConfig.ConnectionString;

    }
}