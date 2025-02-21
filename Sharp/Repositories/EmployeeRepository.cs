using Microsoft.EntityFrameworkCore;
using Sharp.Data;
using Sharp.Models;
using Sharp.ViewModels;

namespace Sharp.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<EmployeeRepository> _logger;

        public EmployeeRepository(AppDbContext context, ILogger<EmployeeRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<Employee>> GetAllEmployeesAsync()
        {
            return await _context.Employees.Include(e => e.Salaries).ToListAsync();
        }
        public async Task<List<TitleSalaryViewModel>> GetTitlesAsync()
        {

            var salaries = await _context.EmployeeSalaries
                .Include(s => s.Employee) // Include related employee data if needed
                .ToListAsync();

            var result = salaries
                .GroupBy(s => s.Title)
                .Select(g => new TitleSalaryViewModel
                {
                    Title = g.Key,
                    MinSalary = g.Min(s => s.Salary),
                    MaxSalary = g.Max(s => s.Salary)
                })
                .ToList();

            return result;
        }

        public async Task<List<Employee>> GetEmployeesByFiltersAsync(string name, string title)
        {
            var query = _context.Employees.Include(e => e.Salaries).AsQueryable();

            if (!string.IsNullOrEmpty(name))
                query = query.Where(e => e.Name.Contains(name));

            if (!string.IsNullOrEmpty(title))
                query = query.Where(e => e.Salaries.Any(s => s.Title == title));

            return await query.ToListAsync();
        }

        public async Task AddEmployeeAsync(Employee employee)
        {

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
        }
    }
}

