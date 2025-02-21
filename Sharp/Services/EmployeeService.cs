using Sharp.Models;
using Sharp.Repositories;
using Sharp.ViewModels;

namespace Sharp.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _repository;
        private readonly ILogger<EmployeeService> _logger;

        public EmployeeService(IEmployeeRepository repository, ILogger<EmployeeService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<List<Employee>> GetAllEmployeesAsync()
        {
            try
            {
                return await _repository.GetAllEmployeesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in EmployeeService.GetAllEmployeesAsync");
                throw new ApplicationException("An error occurred while fetching employees.");
            }
        }

        public async Task<List<Employee>> GetEmployeesByFiltersAsync(string name, string title)
        {
            try
            {
                return await _repository.GetEmployeesByFiltersAsync(name, title);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in EmployeeService.GetEmployeesByFiltersAsync");
                throw new ApplicationException("An error occurred while searching employees.");
            }
        }



        public async Task<List<TitleSalaryViewModel>> GetTitlesAsync()
        {
            try
            {
                return await _repository.GetTitlesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in EmployeeService.GetTitlesAsync");
                throw new ApplicationException("An error occurred while fetching titles.");
            }
        }

        public async Task AddEmployeeAsync(Employee employee)
        {
            try
            {
                await _repository.AddEmployeeAsync(employee);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in EmployeeService.AddEmployeeAsync");
                throw new ApplicationException("An error occurred while adding the employee.");
            }
        }
    }
}
