using Server.DAL.Configuration;

namespace Server.DAL.Repository
{
    public abstract class  BaseRepository
    {
        public readonly DatabaseConfig DbConfig = new DatabaseConfig();
        public string ConnectionString => DbConfig.ConnectionString;

    }
}