using Sharp.Models;
using Sharp.ViewModels;

namespace Sharp.Services
{
    public interface IEmployeeService
    {
        Task<List<Employee>> GetAllEmployeesAsync();
        Task<List<Employee>> GetEmployeesByFiltersAsync(string name, string title);
        Task<List<TitleSalaryViewModel>> GetTitlesAsync();
        Task AddEmployeeAsync(Employee employee);
    }
}
