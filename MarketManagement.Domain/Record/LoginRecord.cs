using System.Diagnostics.CodeAnalysis;

namespace MarketManagement.Domain.Record
{
    [ExcludeFromCodeCoverage]
    public record LoginRecord(string email, string token);
}
