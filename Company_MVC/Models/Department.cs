using System.ComponentModel.DataAnnotations;

namespace Company_MVC.Models
{
    public class Department
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public List<Employee>? Employees { get; set; } = new List<Employee>();
    }
}
