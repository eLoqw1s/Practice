using Microsoft.AspNetCore.Mvc;
using Practice.Application.DTO;
using Practice.Application.Interfaces;
using Practice.WebApi.Contracts;

namespace Practice.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PracticeController : ControllerBase
    {
        private readonly IPracticeService _practiceService;

        public PracticeController(IPracticeService practiceService)
        {
            _practiceService = practiceService;
        }

        [HttpGet("test_query")]
        public async Task<ActionResult<List<TestDetailsVm>>> GetAllRegions()
        {
            var regions = await _practiceService.GetRegions();
            var response = regions.Select(b => new TestDetailsVm(b.RegionId, b.RegionName));
            return Ok(response);
        }

        [HttpGet("first_query")]
        public async Task<ActionResult<List<EmployeeDetailsVm>>> GetEmployeesWithManagersHiredIn2023()
        {
            var entity = await _practiceService.GetEmployeesWithManagersHiredIn2023();
            var response = entity.Select(b=>new EmployeeDetailsVm(b.FirstName, b.LastName, b.Email, b.PhoneNumber,
                b.HireDate, b.Salary, b.CommissionPct));
            return Ok(response);
        }

        [HttpGet("second_query")]
        public async Task<ActionResult<List<EmployeeInfoThreeFieldsDto>>> GetDepartmentAndJobEmployees()
        {
            var entity = await _practiceService.GetDepartmentAndJobEmployees();
            return Ok(entity);
        }

        [HttpGet("third_query")]
        public async Task<ActionResult<string>> GetCityWithLowestTotalSalary()
        {
            var city = await _practiceService.GetCityWithLowestTotalSalary();
            return Ok(city);
        }

        [HttpGet("fourth_query")]
        public async Task<ActionResult<List<EmployeeDetailsVm>>> GetEmployeesWithManagersHiredInJanuaryAndTitleMore15()
        {
            var entity = await _practiceService.GetEmployeesWithManagersHiredInJanuaryAndTitleMore15();
            var response = entity.Select(b => new EmployeeDetailsVm(b.FirstName, b.LastName, b.Email, b.PhoneNumber,
                b.HireDate, b.Salary, b.CommissionPct));
            return Ok(response);
        }

        [HttpGet("fifth_query")]
        public async Task<ActionResult<List<string>>> GetFirstNamesOfEmployeesInEurope()
        {
            var firstName = await _practiceService.GetFirstNamesOfEmployeesInEurope();
            return Ok(firstName);
        }

        [HttpGet("sixth_query")]
        public async Task<ActionResult<List<EmployeeDetailDto>>> GetEmployeeDetails()
        {
            var employeeDetails = await _practiceService.GetEmployeeDetails();
            return Ok(employeeDetails);
        }

        [HttpGet("seventh_query")]
        public async Task<ActionResult<List<RegionEmployeeCountDto>>> GetRegionsWithEmployeeCount()
        {
            var employeeCount = await _practiceService.GetRegionsWithEmployeeCount();
            return Ok(employeeCount);
        }

        [HttpGet("eighth_query")]
        public async Task<ActionResult<List<DepartmentMinMaxDto>>> GetDepartmentSalaryAndEmployeeStats()
        {
            var employeeStats = await _practiceService.GetDepartmentSalaryAndEmployeeStats();
            return Ok(employeeStats);
        }

        [HttpGet("ninth_query")]
        public async Task<ActionResult<List<EmployeePhoneFormat>>> GetEmployeesPhoneInfo()
        {
            var employeesByPhoneFormat = await _practiceService.GetEmployeesPhoneInfo();
            return Ok(employeesByPhoneFormat);
        }

        [HttpGet("tenth_query")]
        public async Task<ActionResult<List<EmployeeDetailsVm>>> GetEmployeesInDepartmentAdministration()
        {
            var employeesInDepartmentAdmin = await _practiceService.GetEmployeesInDepartmentAdministration();
            var response = employeesInDepartmentAdmin.Select(b => new EmployeeDetailsVm(b.FirstName,
                b.LastName, b.Email, b.PhoneNumber,
                b.HireDate, b.Salary, b.CommissionPct));
            return Ok(response);
        }
    }
}
