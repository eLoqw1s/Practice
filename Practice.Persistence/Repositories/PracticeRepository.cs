using Microsoft.EntityFrameworkCore;
using Practice.Application.DTO;
using Practice.Application.Interfaces;
using Practice.Domain.Models;

namespace Practice.Persistence.Repositories
{
    public class PracticeRepository : IPracticeRepository
    {
        private readonly PracticeDbContext _context;

        public PracticeRepository(PracticeDbContext context)
        {
            _context = context;
        }

        //Test
        public async Task<List<Region>> Get()
        {
            var regions = await _context.Regions
                .AsNoTracking()
                .ToListAsync();
            return regions;
        }

        // 1. Отобразить реквизиты сотрудников, менеджеры которых устроились на работу в 2023 г.,
        //    но при это сами эти работники устроились на работу до 2023 г.
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

        // 2. Отобразить данные по сотрудникам: из какого департамента
        //    и какими текущими задачами они занимаются.
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

        // 3. Отобразить город, в котором сотрудники в сумме зарабатывают меньше всех.
        public async Task<string> GetCityWithLowestTotalSalaryAsync()
        {
            var cityWithLowestSalary = await _context.Locations
                .Join(
                    _context.Departments,
                    location => location.LocationId,
                    department => department.LocationId,
                    (location, department) => new { location, department }
                )
                .Join(
                    _context.Employees,
                    ld => ld.department.DepartmentId,
                    employee => employee.DepartmentId,
                    (ld, employee) => new { ld.location, employee }
                )
                .GroupBy(x => x.location.City)
                .Select(g => new
                {
                    City = g.Key,
                    TotalSalary = g.Sum(x => x.employee.Salary)
                })
                .OrderBy(x => x.TotalSalary)
                .Select(x => x.City)
                .FirstOrDefaultAsync();

            return cityWithLowestSalary;
        }

        // 4. Вывести все реквизиты сотрудников менеджеры которых устроились на работу в январе
        //    месяце любого года и длина job_title этих сотрудников больше 15ти символов
        public async Task<List<Employee>> GetEmployeesWithManagersHiredInJanuaryAndTitleMore15()
        {
            var employees = await _context.Employees
            .Where(e => e.ManagerId.HasValue) 
            .Join(
                _context.Employees,
                employee => employee.ManagerId,
                manager => manager.EmployeeId, 
                (employee, manager) => new { employee, manager } 
            )
            .Join(
                _context.Jobs,
                joined => joined.manager.JobId,
                job => job.JobId,
                (joined, job) => new { joined.employee, joined.manager, JobTitle = job.JobTitle } 
            )
            .Where(joined => joined.manager.HireDate.Month == 1 && joined.JobTitle.Length > 15) 
            .Select(joined => joined.employee) 
            .ToListAsync(); 

            return employees;
        }

        // 5. Вывести реквизит first_name сотрудников, которые живут в Europe (region_name)
        public async Task<List<string>> GetFirstNamesOfEmployeesInEuropeAsync()
        {
            var firstNames = await _context.Employees
                .Join(
                    _context.Departments,
                    e => e.DepartmentId,
                    d => d.DepartmentId,
                    (e, d) => new { Employee = e, Department = d }
                )
                .Join(
                    _context.Locations,
                    ed => ed.Department.LocationId,
                    l => l.LocationId,
                    (ed, l) => new { ed.Employee, ed.Department, Location = l }
                )
                .Join(
                    _context.Countries,
                    edl => edl.Location.CountryId,
                    c => c.CountryId,
                    (edl, c) => new { edl.Employee, edl.Department, edl.Location, Country = c }
                )
                .Join(
                    _context.Regions,
                    edlc => edlc.Country.RegionId,
                    r => r.RegionId,
                    (edlc, r) => new { edlc.Employee, edlc.Department, edlc.Location, edlc.Country, Region = r }
                )
                .Where(r => r.Region.RegionName == "Europe")
                .Select(e => e.Employee.FirstName)
                .ToListAsync();

            return firstNames;
        }

        // 6. Получить детальную информацию о каждом сотруднике:
        //    First_name, Last_name, Departament, Job, Street, Country, Region

        // 7. Отразить регионы и количество сотрудников в каждом из них.

        // 8. Вывести информацию по департаменту department_name с минимальной и максимальной зарплатой,
        //    с ранней и поздней датой прихода на работу и с количеством сотрудников.
        //    Сортировать по количеству сотрудников (по убыванию)

        // 9. Получить список реквизитов сотрудников FIRST_NAME, LAST_NAME
        //    и первые три цифры от номера телефона, если номер в формате ХХХ.ХХХ.ХХХХ

        // 10. Вывести список сотрудников, которые работают в департаменте администрирования
        //     доходов (departments.department_name = 'DAD')
    }
}
