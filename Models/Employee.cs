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
        public bool IsActive { get; set; } = true; // статус сотрудника (активный/уволенный).
    }


    public class EmployeeDto : Person
    {
        public int DepartmentId { get; set; }
        public string Department { get; set; } // Название департамента
        public int PositionId { get; set; }
        public string Position { get; set; }   // Название должности
        public DateTime HireDate { get; set; } // Дата устройства на работу

        //public override bool Equals(object? obj)
        //{
        //    if (obj is EmployeeDto other)
        //    {
        //        return Id == other.Id &&
        //               Name == other.Name &&
        //               Surname == other.Surname &&
        //               Patronymic == other.Patronymic &&
        //               DepartmentId == other.DepartmentId &&
        //               Department == other.Department &&
        //               PositionId == other.PositionId &&
        //               Position == other.Position &&
        //               HireDate == other.HireDate;
        //    }
        //    return false;
        //}

        //public override int GetHashCode()
        //{
        //    return HashCode.Combine(
        //        Id,
        //        HashCode.Combine(Name, Surname, Patronymic),
        //        HashCode.Combine(DepartmentId, Department),
        //        HashCode.Combine(PositionId, Position, HireDate)
        //    );
        //}
    }
}
