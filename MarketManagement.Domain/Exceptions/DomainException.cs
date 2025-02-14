namespace MarketManagement.Domain.Exceptions
{
    public class DomainException(string error) : Exception(error)
    {
        public static void When(bool hasError, string error)
        {
            if (hasError)
            {
                throw new DomainException(error);
            }
        }
    }
}
