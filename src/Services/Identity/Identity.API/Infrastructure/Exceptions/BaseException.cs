using System.Net;

namespace Identity.API.Infrastructure.Exceptions
{
    public class BaseException : Exception
    {
        public IEnumerable<string> ErrorMessages { get; }

        public HttpStatusCode StatusCode { get; }

        public BaseException(string message, IEnumerable<string> errors, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
            : base(message)
        {
            ErrorMessages = errors;
            StatusCode = statusCode;
        }

        public BaseException(string message) : base(message)
        {
            ErrorMessages = new List<string>();
        }
    }
}
