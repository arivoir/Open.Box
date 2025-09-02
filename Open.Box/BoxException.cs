using System.Net;

namespace Open.Box;

public class BoxException : Exception
{

    public BoxException(HttpStatusCode statusCode, Error error)
    {
        StatusCode = statusCode;
        Error = error;
    }

    public HttpStatusCode StatusCode { get; private set; }
    public Error Error { get; private set; }
}
