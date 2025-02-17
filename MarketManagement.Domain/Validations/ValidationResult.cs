namespace MarketManagement.Domain.Validations
{
    public class ValidationResult
    {
        public bool IsValid => !Errors.Any();
        public List<string> Errors { get; } = new();
        public void AddError(string error) => Errors.Add(error);
    }
}
