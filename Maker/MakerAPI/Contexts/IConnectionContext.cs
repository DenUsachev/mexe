using MakerAPI.Domain;
using MakerAPI.Helpers;

namespace MakerAPI.Contexts;

public interface IConnectionContext
{
    public bool OpenContext();
    public Task<ApiCallResult<PortfolioInfo>> GetPortfolioInfo();
    public Task<ApiCallResult<IEnumerable<PositionInfo>>> GetOpenPositions();
}