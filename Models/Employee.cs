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
        public DateOnly DateOfDismissal { get; set; } // Дата увольнения
        public bool IsActive { get; set; } = true; // статус сотрудника (активный/уволенный).
    }


    public class EmployeeDto : Person, IEquatable<EmployeeDto>
    {
        public int DepartmentId { get; set; }
        public string Department { get; set; } // Название департамента
        public int PositionId { get; set; }
        public string Position { get; set; }   // Название должности
        public DateTime HireDate { get; set; } // Дата устройства на работу

        public bool Equals(EmployeeDto? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return Id == other.Id &&
                   DepartmentId == other.DepartmentId &&
                   Department == other.Department &&
                   PositionId == other.PositionId &&
                   Position == other.Position &&
                   HireDate == other.HireDate;
        }

        public override bool Equals(object? obj)
        {
            return obj is EmployeeDto other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, DepartmentId, Department, PositionId, Position, HireDate);
        }
    }
}
