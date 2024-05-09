using MakerAPI.Helpers;

namespace MakerAPI.Domain;

public interface IConnectionContext
{
    public bool OpenContext();
    public Task<ApiCallResult<decimal>> GetAccountTotals();
}