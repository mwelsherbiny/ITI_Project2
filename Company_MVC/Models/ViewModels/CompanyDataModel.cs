namespace Company_MVC.Models.ViewModels
{
    public class CompanyDataModel
    {
        public required string Name { get; set; }
        public int TotalEmployees { get; set; }
        public int TotalDepartments { get; set; }
        public double TotalSalary { get; set; }
    }
}
