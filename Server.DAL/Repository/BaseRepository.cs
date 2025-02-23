using Server.DAL.Configuration;

namespace Server.DAL.Repository;

public class BaseRepository
{
    protected DatabaseConfig dbConfig = new DatabaseConfig();
}