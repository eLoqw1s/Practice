using Practice.Application.DTO;
using Practice.Application.Interfaces;
using Practice.Domain.Models;

namespace Practice.Application.Services
{
    public class PracticeService : IPracticeService
    {
        private readonly IPracticeRepository _practiceRepository;

        public PracticeService(IPracticeRepository practiceRepository)
        {
            _practiceRepository = practiceRepository;
        }

        public async Task<List<Region>> GetRegions()
        {
            return await _practiceRepository.Get();
        }

        public async Task<List<Employee>> GetEmployeesWithManagersHiredIn2023()
        {
            return await _practiceRepository.GetEmployeesWithManagersHiredIn2023();
        }
        public async Task<List<EmployeeInfoThreeFieldsDto>> GetDepartmentAndJobEmployees()
        {
            return await _practiceRepository.GetDepartmentAndJobEmployees();
        }
        public async Task<string> GetCityWithLowestTotalSalary()
        {
            return await _practiceRepository.GetCityWithLowestTotalSalary();
        }
        public async Task<List<Employee>> GetEmployeesWithManagersHiredInJanuaryAndTitleMore15()
        {
            return await _practiceRepository.GetEmployeesWithManagersHiredInJanuaryAndTitleMore15();
        }

        public async Task<List<string>> GetFirstNamesOfEmployeesInEurope()
        {
            return await _practiceRepository.GetFirstNamesOfEmployeesInEurope();
        }
        public async Task<List<EmployeeDetailDto>> GetEmployeeDetails()
        {
            return await _practiceRepository.GetEmployeeDetails();
        }
        public async Task<List<RegionEmployeeCountDto>> GetRegionsWithEmployeeCount()
        {
            return await _practiceRepository.GetRegionsWithEmployeeCount();
        }
        public async Task<List<DepartmentMinMaxDto>> GetDepartmentSalaryAndEmployeeStats()
        {
            return await _practiceRepository.GetDepartmentSalaryAndEmployeeStats();
        }
        public async Task<List<EmployeePhoneFormat>> GetEmployeesPhoneInfo()
        {
            return await _practiceRepository.GetEmployeesPhoneInfo();
        }
        public async Task<List<Employee>> GetEmployeesInDepartmentAdministration()
        {
            return await _practiceRepository.GetEmployeesInDepartmentAdministration();
        }
    }
}
