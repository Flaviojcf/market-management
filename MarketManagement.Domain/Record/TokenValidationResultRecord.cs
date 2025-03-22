using System.Diagnostics.CodeAnalysis;

namespace MarketManagement.Domain.Record
{
    [ExcludeFromCodeCoverage]

    public record TokenValidationResultRecord(bool IsValid, string Email);
}
