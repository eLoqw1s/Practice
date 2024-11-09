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
                      e => e.ManagerId,
                      m => m.EmployeeId,
                      (e, m) => new { Employee = e, Manager = m })
                .Where(em => em.Manager.HireDate >= new DateTime(2023, 1, 1)
                              && em.Manager.HireDate < new DateTime(2024, 1, 1))
                .Select(em => em.Employee)
                .ToListAsync();

            return result;
        }

        // 2. Отобразить данные по сотрудникам: из какого департамента
        //    и какими текущими задачами они занимаются.
        public async Task<List<EmployeeInfoThreeFieldsDto>> GetDepartmentAndJobEmployees()
        {
            var employeeInfo = await _context.Employees
                .Join(
                    _context.Departments,
                    e => e.DepartmentId,
                    d => d.DepartmentId,
                    (e, d) => new { Employee = e, Department = d }
                )
                .Join(
                    _context.Jobs,
                    ed => ed.Employee.JobId,
                    j => j.JobId,
                    (ed, j) => new EmployeeInfoThreeFieldsDto
                    (
                        ed.Employee.FirstName,
                        j.JobTitle,
                        ed.Department.DepartmentName
                    )
                )
                .ToListAsync();

            return employeeInfo;
        }

        // 3. Отобразить город, в котором сотрудники в сумме зарабатывают меньше всех.
        public async Task<string> GetCityWithLowestTotalSalary()
        {
            var cityWithLowestSalary = await _context.Locations
                .Join(
                    _context.Departments,
                    l => l.LocationId,
                    d => d.LocationId,
                    (l, d) => new { Location = l, Department = d }
                )
                .Join(
                    _context.Employees,
                    ld => ld.Department.DepartmentId,
                    e => e.DepartmentId,
                    (ld, e) => new { ld.Location, Employee = e }
                )
                .GroupBy(x => x.Location.City)
                .Select(g => new
                {
                    City = g.Key,
                    TotalSalary = g.Sum(x => x.Employee.Salary)
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
                e => e.ManagerId,
                m => m.EmployeeId, 
                (e, m) => new { Employee = e, Manager = m } 
            )
            .Join(
                _context.Jobs,
                em => em.Employee.JobId,
                j => j.JobId,
                (em, j) => new { em.Employee, em.Manager, JobTitle = j.JobTitle } 
            )
            .Where(emj => emj.Manager.HireDate.Month == 1 && emj.JobTitle.Length > 15) 
            .Select(emj => emj.Employee) 
            .ToListAsync(); 

            return employees;
        }

        // 5. Вывести реквизит first_name сотрудников, которые живут в Europe (region_name)
        public async Task<List<string>> GetFirstNamesOfEmployeesInEurope()
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
        public async Task<List<EmployeeDetailDto>> GetEmployeeDetails()
        {
            var employeeDetails = await _context.Employees
                .Join(
                    _context.Jobs,
                    e => e.JobId,
                    j => j.JobId,
                    (e, j) => new { Employee = e, Job = j }
                )
                .Join(
                    _context.Departments,
                    ej => ej.Employee.DepartmentId,
                    d => d.DepartmentId,
                    (ej, d) => new { ej.Employee, ej.Job, Department = d }
                )
                .Join(
                    _context.Locations,
                    ejd => ejd.Department.LocationId,
                    l => l.LocationId,
                    (ejd, l) => new { ejd.Employee, ejd.Job, ejd.Department, Location = l }
                )
                .Join(
                    _context.Countries,
                    ejdl => ejdl.Location.CountryId,
                    c => c.CountryId,
                    (ejdl, c) => new { ejdl.Employee, ejdl.Job, ejdl.Department, ejdl.Location, Country = c }
                )
                .Join(
                    _context.Regions,
                    ejdlc => ejdlc.Country.RegionId,
                    r => r.RegionId,
                    (ejdlc, r) => new EmployeeDetailDto
                    (
                        ejdlc.Employee.FirstName,
                        ejdlc.Employee.LastName,
                        ejdlc.Department.DepartmentName,
                        ejdlc.Job.JobTitle,
                        ejdlc.Location.StreetAddress,
                        ejdlc.Country.CountryName,
                        r.RegionName
                    )
                )
                .ToListAsync();

            return employeeDetails;
        }

        // 7. Отразить регионы и количество сотрудников в каждом из них.
        public async Task<List<RegionEmployeeCountDto>> GetRegionsWithEmployeeCount()
        {
            var regionCounts = await _context.Regions
                .GroupJoin(
                    _context.Countries,
                    r => r.RegionId,
                    c => c.RegionId,
                    (r, c) => new { Region = r, Country = c }
                )
                .SelectMany(
                    rc => rc.Country.DefaultIfEmpty(),
                    (rc, country) => new { rc.Region, country }
                )
                .GroupJoin(
                    _context.Locations,
                    rc => rc.country.CountryId,
                    l => l.CountryId,
                    (rc, l) => new { rc.Region, rc.country, Locations = l }
                )
                .SelectMany(
                    rcl => rcl.Locations.DefaultIfEmpty(),
                    (rcl, location) => new { rcl.Region, rcl.country, location }
                )
                .GroupJoin(
                    _context.Departments,
                    rcl => rcl.location.LocationId,
                    d => d.LocationId,
                    (rcl, d) => new { rcl.Region, rcl.country, rcl.location, Departments = d }
                )
                .SelectMany(
                    rcld => rcld.Departments.DefaultIfEmpty(),
                    (rcld, department) => new { rcld.Region, rcld.country, rcld.location, department }
                )
                .GroupJoin(
                    _context.Employees,
                    rcldd => rcldd.department.DepartmentId,
                    e => e.DepartmentId,
                    (rcldd, e) => new { rcldd.Region, rcldd.country, rcldd.location, rcldd.department, Employees = e }
                )
                .SelectMany(
                    rcldde => rcldde.Employees.DefaultIfEmpty(),
                    (rcldde, employee) => new { rcldde.Region, employee }
                )
                .GroupBy(r => r.Region.RegionName)
                .Select(g => new RegionEmployeeCountDto
                (
                    g.Key,
                    g.Count(e => e.employee != null)
                ))
                .ToListAsync();

            return regionCounts;
        }

        // 8. Вывести информацию по департаменту department_name с минимальной и максимальной зарплатой,
        //    с ранней и поздней датой прихода на работу и с количеством сотрудников.
        //    Сортировать по количеству сотрудников (по убыванию)
        public async Task<List<DepartmentMinMaxDto>> GetDepartmentSalaryAndEmployeeStats()
        {
            var departmentStats = await _context.Departments
                .Join(
                    _context.Employees,
                    d => d.DepartmentId,
                    e => e.DepartmentId,
                    (d, e) => new { Department = d, Employee = e }
                )
                .GroupBy(de => de.Department.DepartmentName)
                .Select(g => new DepartmentMinMaxDto
                (
                    g.Key,
                    g.Min(x => x.Employee.Salary),
                    g.Max(x => x.Employee.Salary),
                    g.Min(x => x.Employee.HireDate),
                    g.Max(x => x.Employee.HireDate),
                    g.Count()
                ))
                .ToListAsync();

            return departmentStats.OrderByDescending(ds => ds.EmployeeCount).ToList();
        }

        // 9. Получить список реквизитов сотрудников FIRST_NAME, LAST_NAME
        //    и первые три цифры от номера телефона, если номер в формате ХХХ.ХХХ.ХХХХ
        public async Task<List<EmployeePhoneFormat>> GetEmployeesPhoneInfo()
        {
            var employeePhoneInfo = await _context.Employees
                .Where(e => EF.Functions.Like(e.PhoneNumber, "___.___.____"))
                .Select(e => new EmployeePhoneFormat
                (
                    e.FirstName,
                    e.LastName,
                    e.PhoneNumber.Substring(0, 3)
                ))
                .ToListAsync();

            return employeePhoneInfo;
        }

        // 10. Вывести список сотрудников, которые работают в департаменте администрирования
        //     доходов (departments.department_name = 'DAD')
        public async Task<List<Employee>> GetEmployeesInDepartmentAdministration()
        {
            var employees = await _context.Employees
                .Join(
                    _context.Departments,
                    e => e.DepartmentId,
                    d => d.DepartmentId,
                    (e, d) => new { Employee = e, Department = d }
                )
                .Where(ed => ed.Department.DepartmentName == "DAD")
                .Select(ed => ed.Employee)
                .ToListAsync();

            return employees;
        }
    }
}
