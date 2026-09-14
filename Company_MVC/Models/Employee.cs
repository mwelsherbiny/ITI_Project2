using Microsoft.Identity.Client;

namespace Company_MVC.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required int Age { get; set; }
        public int Salary { get; set; } = 5000;
        public Department? Department { get; set; }
        public int? DepartmentId { get; set; }
    }
}
