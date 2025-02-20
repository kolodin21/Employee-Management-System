namespace Models
{
    public class Person
    {
        public int PersonId { get; set; }             // Id сотрудника
        public string Name { get; set; }        // Имя
        public string Surname { get; set; }     // Фамилия
        public string? Patronymic { get; set; } // Может быть null
    }
    
    public class Employee : Person
    {
        public int EmployeeId { get; set; }
        public int DepartmentId { get; set; } 
        public int PositionId { get; set; }
        public DateTime HireDate { get; set; } // Дата устройства на работу
        public DateTime DateOfDismissal { get; set; }    // статус сотрудника (активный/уволенный).
    }
    
    public class EmployeeDto : Person, IEquatable<EmployeeDto>
    {
        
        public int EmployeeId { get; set; }
        public int DepartmentId { get; set; }
        public string Department { get; set; } // Название департамента
        public int PositionId { get; set; }
        public string Position { get; set; }   // Название должности
        public DateTime HireDate { get; set; } // Дата устройства на работу
        public DateTime DateOfDismissal { get; set; }

        public bool Equals(EmployeeDto? other)
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object? obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((EmployeeDto)obj);
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
        
    }
}
