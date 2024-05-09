using Bybit.Net.Objects.Models.V5;

namespace MakerAPI.Domain
{
    public class PortfolioInfo
    {
        public IEnumerable<BybitAssetAccountBalance> Assets { get; set; }
        public decimal UsdTotal { get; set; }
    }
}
