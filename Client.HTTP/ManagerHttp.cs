namespace Client.HTTP
{
    public class ManagerHttp
    (
        EmployeeHttpClient employeeHttpClient,
        DepartmentHttpClient departmentHttpClient,
        PositionHttpClient positionHttpClient,
        ReportHttpClient reportHttpClient,
        LeaveHttpClient leaveHttpClient
        )
    {
        public EmployeeHttpClient EmployeeHttpClient { get; private set; } = employeeHttpClient;
        public DepartmentHttpClient DepartmentHttpClient { get; private set; } = departmentHttpClient;
        public PositionHttpClient PositionHttpClient { get; private set; } = positionHttpClient;
        public ReportHttpClient ReportHttpClient { get; private set; } = reportHttpClient;
        public LeaveHttpClient LeaveHttpClient { get; private set; } = leaveHttpClient;
    }
}
