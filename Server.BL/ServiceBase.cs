using Server.DAL.Repository;

namespace Server.BL
{
    public abstract class ServiceBase
    {
        public static RepositoryManager RepositoryManager = new();
    }
}
