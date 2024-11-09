namespace Practice.Application.DTO
{
    public record DepartmentMinMaxDto
    (
        string DepartmentName,
        decimal? MinSalary,
        decimal? MaxSalary,
        DateTime EarliestHireDate,
        DateTime LatestHireDate,
        int EmployeeCount
    );
}
