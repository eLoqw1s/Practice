using Practice.Application.DTO;
using Practice.Domain.Models;

namespace Practice.Application.Interfaces
{
    public interface IPracticeRepository
    {
        Task<List<Region>> Get();
        Task<List<Employee>> GetEmployeesWithManagersHiredIn2023();
        Task<List<EmployeeInfoThreeFieldsDto>> GetDepartmentAndJobEmployees();
        Task<string> GetCityWithLowestTotalSalary();
        Task<List<Employee>> GetEmployeesWithManagersHiredInJanuaryAndTitleMore15();
        Task<List<string>> GetFirstNamesOfEmployeesInEurope();
        Task<List<EmployeeDetailDto>> GetEmployeeDetails();
        Task<List<RegionEmployeeCountDto>> GetRegionsWithEmployeeCount();
        Task<List<DepartmentMinMaxDto>> GetDepartmentSalaryAndEmployeeStats();
        Task<List<EmployeePhoneFormat>> GetEmployeesPhoneInfo();
        Task<List<Employee>> GetEmployeesInDepartmentAdministration();
    }
}