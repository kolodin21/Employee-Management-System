namespace Server.DAL.Repository
{
    public class RepositoryManager
    {
        public DepartmentRepository DepartmentRepository { get; }
        public EmployeeRepository EmployeeRepository { get; }
        public LeaveRepository LeaveRepository { get; }
        public PositionRepository PositionRepository { get; }
        public ReportSearchRepository ReportSearchRepository { get; }

        public RepositoryManager()
        {
            DepartmentRepository = new DepartmentRepository();
            EmployeeRepository = new EmployeeRepository();
            LeaveRepository = new LeaveRepository();
            PositionRepository = new PositionRepository();
            ReportSearchRepository = new ReportSearchRepository();
        }
    }
}
