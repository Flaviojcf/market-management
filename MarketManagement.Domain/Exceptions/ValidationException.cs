namespace MarketManagement.Domain.Exceptions
{
    public class ValidationException : Exception
    {
        public List<string> Errors { get; } = new List<string>();

        public ValidationException(List<string> errors) : base("Erros de validação ocorreram.")
        {
            Errors = errors;
        }

        public ValidationException(string error) : base(error)
        {
            Errors.Add(error);
        }
    }
}
