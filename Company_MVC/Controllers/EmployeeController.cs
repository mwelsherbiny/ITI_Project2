
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Company_MVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Company_MVC.Data;
using Microsoft.AspNetCore.Authorization;

[Authorize]
public class EmployeeController : Controller
{
    private readonly CompanyContext _context;

    public EmployeeController(CompanyContext context)
    {
        _context = context;
    }

    // GET: EMPLOYEES
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Employees.ToListAsync());
    }

    // GET: EMPLOYEES/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var employee = await _context.Employees
            .Include(e => e.Department)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (employee == null)
        {
            return NotFound();
        }

        return View(employee);
    }

    // GET: EMPLOYEES/Create
    [Authorize(Roles = "Admin")]
    public IActionResult Create()
    {
        ViewBag.Departments = new SelectList(
            _context.Departments,
            "Id",
            "Name"
        );
        return View();
    }

    // POST: EMPLOYEES/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([Bind("Id,Name,Salary,Age,DepartmentId")] Employee employee)
    {
        if (ModelState.IsValid)
        {
            _context.Add(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        foreach (var error in ModelState)
        {
            foreach (var msg in error.Value.Errors)
            {
                Console.WriteLine($"{error.Key}: {msg.ErrorMessage}");
            }
        }

        ViewBag.Departments = new SelectList(
            _context.Departments,
            "Id",
            "Name",
            employee.DepartmentId
        );

        return View(employee);
    }

    // GET: EMPLOYEES/Edit/5
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var employee = await _context.Employees.FindAsync(id);
        if (employee == null)
        {
            return NotFound();
        }

        ViewBag.Departments = new SelectList(
            _context.Departments,
            "Id",
            "Name"
        );
        return View(employee);
    }

    // POST: EMPLOYEES/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Name,Salary,Age,Department,DepartmentId")] Employee employee)
    {
        if (id != employee.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(employee);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(employee.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(employee);
    }

    // GET: EMPLOYEES/Delete/5
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var employee = await _context.Employees
            .FirstOrDefaultAsync(m => m.Id == id);
        if (employee == null)
        {
            return NotFound();
        }

        return View(employee);
    }

    // POST: EMPLOYEES/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var employee = await _context.Employees.FindAsync(id);
        if (employee != null)
        {
            _context.Employees.Remove(employee);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Search(string name)
    {
        var employees = await _context.Employees.Where(e => e.Name.StartsWith(name) ).ToListAsync();
        return Json(employees);
    }

    private bool EmployeeExists(int? id)
    {
        return _context.Employees.Any(e => e.Id == id);
    }
}
