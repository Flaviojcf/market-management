using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace MarketManagement.Domain.Validations
{
    [ExcludeFromCodeCoverage]
    public class ErrorValidation
    {
        public ErrorValidation(HttpStatusCode status)
        {
            TraceId = Guid.NewGuid().ToString();
            Status = status;
            Errors = [];
        }

        public ErrorValidation(string message, HttpStatusCode status)
        {
            TraceId = Guid.NewGuid().ToString();
            Status = status;
            Errors = [];
            AddError(message);
        }

        public string TraceId { get; private set; }
        public HttpStatusCode Status { get; set; }
        public List<ErrorDetails> Errors { get; private set; }

        public void AddError(string message)
        {
            Errors.Add(new ErrorDetails(message));
        }

        public class ErrorDetails(string message)
        {
            public string Message { get; private set; } = message;
        }
    }
}
