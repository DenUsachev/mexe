using System.Net;

namespace MakerAPI.Helpers;

public class ApiCallResult<T>
{
    public T? Payload { get; }
    public HttpStatusCode? HttpStatusCode { get; }
    public bool IsOk => (int)HttpStatusCode >= 200 && (int)HttpStatusCode < 300;
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

    public static ApiCallResult<T> CreateOk(T payload, HttpStatusCode code = System.Net.HttpStatusCode.OK)
    {
        return new ApiCallResult<T>(code, payload);
    }

    public static ApiCallResult<T> CreateFailed(string error, HttpStatusCode code = System.Net.HttpStatusCode.InternalServerError)
    {
        return new ApiCallResult<T>(code, error);
    }
}