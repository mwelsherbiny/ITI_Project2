using Company_MVC.Data;
using Company_MVC.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Company_MVC.ViewComponents
{
    public class CompanyDataViewComponent : ViewComponent
    {
        private readonly CompanyContext _context;

        public CompanyDataViewComponent(CompanyContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var companyName = "Company1";
            var totalEmployees = _context.Employees.Count();
            var totalDepartments = _context.Departments.Count();
            var totalSalary = _context.Employees.Sum(e => e.Salary);

            var companyDataModel = new CompanyDataModel
            {
                Name = companyName,
                TotalEmployees = totalEmployees,
                TotalDepartments = totalDepartments,
                TotalSalary = totalSalary
            };

            return View(companyDataModel);
        }
    }
}
