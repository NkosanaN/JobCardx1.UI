using TimeCard.Domain;

namespace TimeCard.Service.Repository.Interfaces;

public interface IEmployeeService
{
    Task<List<Employee>> GetEmployeeAsync();
    Task<Employee> GetEmployeeAsync(string empId);
    Task<Employee> CreateEmployeeAsync(Employee employee);
}
