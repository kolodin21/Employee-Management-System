namespace Models
{
    public class Person
    {
        public int Id { get; set; }             // Id сотрудника
        public string Name { get; set; }        // Имя
        public string Surname { get; set; }     // Фамилия
        public string? Patronymic { get; set; } // Может быть null
    }

    public class Employee : Person
    {
        public int DepartmentId { get; set; } 
        public int PositionId { get; set; }
        public DateTime HireDate { get; set; } // Дата устройства на работу
        public bool IsActive { get; set; }     // статус сотрудника (активный/уволенный).
    }


    public class EmployeeDto : Person
    {
        public int DepartmentId { get; set; }
        public string Department { get; set; } // Название департамента
        public int PositionId { get; set; }
        public string Position { get; set; }   // Название должности
        public DateTime HireDate { get; set; } // Дата устройства на работу
    }
}
