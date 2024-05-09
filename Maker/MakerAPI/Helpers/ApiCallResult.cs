using System.Net;

namespace MakerAPI.Helpers;

public class ApiCallResult<T>
{
    public T? Payload { get; }
    public HttpStatusCode? HttpStatusCode { get; }
    public string? Error { get; }

    private ApiCallResult(HttpStatusCode code, T payload, string? error = null)
    {
        HttpStatusCode = code;
        Payload = payload;
        Error = error;
    }

    private ApiCallResult(HttpStatusCode code, string? error = null)
    {
        HttpStatusCode = code;
        Error = error;
    }

    public static ApiCallResult<T> CreateOk(HttpStatusCode code, T payload)
    {
        return new ApiCallResult<T>(code, payload);
    }

    public static ApiCallResult<T> CreateFailed(HttpStatusCode code, string error)
    {
        return new ApiCallResult<T>(code, error);
    }
}