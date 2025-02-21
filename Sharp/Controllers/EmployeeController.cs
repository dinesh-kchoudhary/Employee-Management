using Microsoft.AspNetCore.Mvc;
using Sharp.Extensions;
using Sharp.Models;
using Sharp.Services;

namespace Sharp.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _service;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(IEmployeeService service, ILogger<EmployeeController> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<IActionResult> Index(string searchName, string searchTitle, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var employees = await _service.GetEmployeesByFiltersAsync(searchName, searchTitle);

                var pagedEmployees = employees.ToPagedList(pageNumber, pageSize);

                return View(pagedEmployees);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in EmployeeController.Index");
                ViewBag.ErrorMessage = "An error occurred while fetching employee data.";
                return View("Error");
            }
        }
        public async Task<IActionResult> Titles(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var titles = await _service.GetTitlesAsync();
                var pagedTitles = titles.ToPagedList(pageNumber, pageSize);

                return View(pagedTitles);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in EmployeeController.Titles");
                ViewBag.ErrorMessage = "An error occurred while fetching titles.";
                return View("Error");
            }
        }
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Employee employee, string Title, decimal Salary)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    
                    employee.Salaries = new List<EmployeeSalary>
            {
                new EmployeeSalary
                {
                    Title = Title,
                    Salary = Salary,
                    FromDate = employee.JoinDate,
                    ToDate = employee.ExitDate ?? DateTime.Now
                }
            };

                    await _service.AddEmployeeAsync(employee);
                    return RedirectToAction("Index");
                }
                return View("Index", await _service.GetAllEmployeesAsync());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in EmployeeController.Add");
                ViewBag.ErrorMessage = "An error occurred while adding the employee.";
                return View("Error");
            }
        }
    }
}
