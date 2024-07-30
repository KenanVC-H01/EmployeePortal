using Microsoft.AspNetCore.Mvc;
using EmployeePortal.Web.Data;
using EmployeePortal.Web.Models;
using EmployeePortal.Web.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeePortal.Web.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public EmployeesController(ApplicationDbContext dbContext) 
        { 
            this.dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddEmployeeViewModel viewModel)
        {
            var employee = new Employee
            {
                empFullName = viewModel.empFullName,
                empDept = viewModel.empDept,
                salary = viewModel.salary,
            };
            await dbContext.Employees.AddAsync(employee);
            await dbContext.SaveChangesAsync();
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var employees = await dbContext.Employees.ToListAsync();

            return View(employees);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id )
        {
            var employee =await dbContext.Employees.FindAsync(id);
            return View(employee);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Employee viewModel)
        {
            var employee = await dbContext.Employees.FindAsync(viewModel.empId);

            if (employee is not null)
            {
                employee.empFullName = viewModel.empFullName;
                employee.empDept = viewModel.empDept;
                employee.salary = viewModel.salary;

                await dbContext.SaveChangesAsync();

            }
            return RedirectToAction("List", "Employees");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Employee viewModel)
        {
            var employee = await dbContext.Employees.AsNoTracking().FirstOrDefaultAsync(x => x.empId == viewModel.empId);

            if (employee is not null)
            {
                dbContext.Employees.Remove(viewModel);
                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "Employees");
        }
    }
}
