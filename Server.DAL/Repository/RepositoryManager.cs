namespace Server.DAL.Repository
{
    public class RepositoryManager
    {
        public DepartmentRepository DepartmentRepository { get; } = new();
        public EmployeeRepository EmployeeRepository { get; } = new();
        public LeaveRepository LeaveRepository { get; } = new();
        public PositionRepository PositionRepository { get; } = new();
        public ReportSearchRepository ReportSearchRepository { get; } = new();
    }
}
