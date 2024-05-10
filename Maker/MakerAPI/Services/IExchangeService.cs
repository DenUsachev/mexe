using MakerAPI.Domain;
using MakerAPI.Helpers;

namespace MakerAPI.Services;

public interface IExchangeService
{
    public bool Run();
    public Task<ApiCallResult<PortfolioInfo>> GetPortfolioStatus();
    public Task<ApiCallResult<IEnumerable<PositionInfo>>> GetPositions();
}