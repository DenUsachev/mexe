using MakerAPI.Contexts;
using MakerAPI.Domain;
using MakerAPI.Helpers;

namespace MakerAPI.Services;

public class BybitService : IExchangeService
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

    public async Task<ApiCallResult<PortfolioInfo>> GetPortfolioStatus()
    {
        return await _ctx.GetPortfolioInfo();
    }

    public async Task<ApiCallResult<IEnumerable<PositionInfo>>> GetPositions()
    {
        return await _ctx.GetOpenPositions();
    }
}