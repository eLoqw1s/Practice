using Practice.Application.DTO;
using Practice.Domain.Models;

namespace Practice.Application.Interfaces
{
    public interface IPracticeRepository
    {
        Task<List<Region>> Get();
        Task<List<Employee>> GetEmployeesWithManagersHiredIn2023();
        Task<List<SecondDto>> GetDepartmentAndJobEmployees();
    }
}