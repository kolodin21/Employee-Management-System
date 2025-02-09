namespace Models
{
    //Тип отсутствия
    public enum LeaveType
    {
        Vacation = 1, //Отпуск
        Medical,      //Больничный
        Dismissed     //Уволен
    }

    public class Leave 
    {
        public int Id { get; set; }             // Id записи
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string? Patronymic { get; set; } // Может быть null
        public LeaveType LeaveType { get; set; } 
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
