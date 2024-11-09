namespace Practice.WebApi.Contracts
{
    public record EmployeeDetailsVm
    (
        string FirstName,
        string SecondName,
        string Email,
        string PhoneNumber,
        DateTime HireDane,
        decimal? Salary,
        decimal? CommissionPct
    );
}
