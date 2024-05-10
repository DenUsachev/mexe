namespace MakerAPI.Domain
{
    public class PortfolioInfo
    {
        public IEnumerable<AssetInfo> Assets { get; set; }
        public decimal UsdTotal { get; set; }
    }
}
