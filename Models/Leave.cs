namespace Models
{
    //Тип отсутствия
    public enum LeaveType
    {
        Vacation = 1, //Отпуск
        Medical,      //Больничный
        Dismissed     //Уволен
    }

    // Учёт отпусков и больничных
    public class Leave 
    {
        public int Id { get; set; }              // Id записи
        public int EmployeeId { get; set; }      // Id сотрудника
        public LeaveType LeaveType { get; set; } // Тип отсутствия 
        public DateTime StartDate { get; set; }  // дата начала.
        public DateTime EndDate { get; set; }    // дата окончания.
    }

    public class LeaveDto : Person
    {
        public int LeaveId { get; set; }         // Id записи
        public LeaveType LeaveType { get; set; } // Тип отсутствия 
        public DateTime StartDate { get; set; }  // дата начала.
        public DateTime EndDate { get; set; }    // дата окончания.

    }
}
 