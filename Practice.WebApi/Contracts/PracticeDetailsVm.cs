using System.ComponentModel.DataAnnotations;

namespace Practice.WebApi.Contracts
{
    public record PracticeDetailsVm
    (
        int RegionId,
        string RegionName
    );
}
