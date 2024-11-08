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
        public async Task<List<SecondDto>> GetDepartmentAndJobEmployees()
        {
            return await _practiceRepository.GetDepartmentAndJobEmployees();
        }
    }
}
