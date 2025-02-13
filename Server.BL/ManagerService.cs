using Server.DAL.Repository;

namespace Server.BL
{
    public class ManagerService
    {
        public DepartmentService DepartmentService { get; }
        public EmployeeService EmployeeService { get; }
        public LeaveService LeaveService { get; }
        public PositionService PositionService { get; }
        public ReportSearchService ReportSearchService { get; }

        public ManagerService(
            DepartmentService departmentService,
            EmployeeService employeeService,
            LeaveService leaveService,
            PositionService positionService,
            ReportSearchService reportSearchService)
        {
            DepartmentService = departmentService;
            EmployeeService = employeeService;
            LeaveService = leaveService;
            PositionService = positionService;
            ReportSearchService = reportSearchService;
        }

    }
}
