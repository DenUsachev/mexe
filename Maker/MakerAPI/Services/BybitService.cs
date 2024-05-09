using MakerAPI.Domain;
using MakerAPI.Helpers;

namespace MakerAPI.Services;

public class BybitService : IBybitService
{
    private readonly IConnectionContext _ctx;

    public BybitService(IConnectionContext ctx)
    {
        _ctx = ctx;
    }

    public bool Run()
    {
        return _ctx.OpenContext();
    }

    public async Task<ApiCallResult<decimal>> GetAccountBalance()
    {
        return await _ctx.GetAccountTotals();
    }
}