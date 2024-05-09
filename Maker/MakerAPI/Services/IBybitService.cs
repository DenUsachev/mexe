using MakerAPI.Helpers;

namespace MakerAPI.Services;

public interface IBybitService
{
    public bool Run();
    public Task<ApiCallResult<decimal>> GetAccountBalance();
}