using Microsoft.EntityFrameworkCore;
using Practice.Application.DTO;
using Practice.Application.Interfaces;
using Practice.Domain.Models;
using System.Linq;

namespace Practice.Persistence.Repositories
{
    public class PracticeRepository : IPracticeRepository
    {
        private readonly PracticeDbContext _context;

        public PracticeRepository(PracticeDbContext context)
        {
            _context = context;
        }

        public async Task<List<Region>> Get()
        {
            var regions = await _context.Regions
                .AsNoTracking()
                .ToListAsync();
            return regions;
        }

        public async Task<List<Employee>> GetEmployeesWithManagersHiredIn2023()
        {
            var result = await _context.Employees
                .Where(e => e.HireDate < new DateTime(2023, 1, 1)
                             && e.ManagerId.HasValue)
                .Join(_context.Employees,
                      employee => employee.ManagerId,
                      manager => manager.EmployeeId,
                      (employee, manager) => new { employee, manager })
                .Where(em => em.manager.HireDate >= new DateTime(2023, 1, 1)
                              && em.manager.HireDate < new DateTime(2024, 1, 1))
                .Select(em => em.employee)
                .ToListAsync();

            return result;
        }

        public async Task<List<SecondDto>> GetDepartmentAndJobEmployees()
        {
            var employeeTasks = await (from e in _context.Employees
                                       join d in _context.Departments on e.DepartmentId equals d.DepartmentId
                                       join j in _context.Jobs on e.JobId equals j.JobId
                                       select new SecondDto(
                                           e.FirstName,
                                           j.JobTitle,
                                           d.DepartmentName
                                       ))
                                       .ToListAsync();

            return employeeTasks;
        }
    }
}
