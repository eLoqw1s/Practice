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
        [HttpGet]
        public async Task<ActionResult<List<PracticeDetailsVm>>> GetAllRegions()
        {
            var regions = await _practiceService.GetRegions();
            var response = regions.Select(b => new PracticeDetailsVm(b.RegionId, b.RegionName));
            return Ok(response);
        }
        [HttpGet("first_query")]
        public async Task<ActionResult<List<FirstQueryDetailsVm>>> GetEmployeesWithManagersHiredIn2023()
        {
            var entity = await _practiceService.GetEmployeesWithManagersHiredIn2023();
            var response = entity.Select(b=>new FirstQueryDetailsVm(b.FirstName, b.LastName, b.Email, b.PhoneNumber,
                b.HireDate, b.Salary, b.CommissionPct));
            return Ok(response);
        }
        [HttpGet("second_query")]
        public async Task<ActionResult<List<SecondDto>>> GetDepartmentAndJobEmployees()
        {
            var entity = await _practiceService.GetDepartmentAndJobEmployees();
            return Ok(entity);
        }
    }
}
