namespace Models
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string? Patronymic { get; set; } // Может быть null
    }

    public class Employee 
    {
        public string Department { get; set; }
        public string Position { get; set; }
        public DateTime HireDate { get; set; }
        public bool IsActive { get; set; }
    }

    public class EmployeeDto
    {



    }
}
