using Server.DAL.Repository;

namespace Server.BL
{
    public abstract class ServiceBase
    {
        public RepositoryManager repositoryManager;
        public ServiceBase()
        {
            repositoryManager = new RepositoryManager();
        }
    }
}
