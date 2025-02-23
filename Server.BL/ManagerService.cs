using Server.DAL.Repository;

namespace Server.BL
{
    public class ManagerService(
        DepartmentService departmentService,
        EmployeeService employeeService,
        LeaveService leaveService,
        PositionService positionService,
        ReportSearchService reportSearchService)
    {
        public DepartmentService DepartmentService { get; } = departmentService;
        public EmployeeService EmployeeService { get; } = employeeService;
        public LeaveService LeaveService { get; } = leaveService;
        public PositionService PositionService { get; } = positionService;
        public ReportSearchService ReportSearchService { get; } = reportSearchService;
    }
}
