namespace MakerAPI.Domain
{
    public class AssetInfo
    {
        /// <summary>Asset symbol</summary>
        public string Symbol { get; set; } = string.Empty;

        /// <summary>Asset wallet balance</summary>
        public Decimal? WalletBalance { get; set; }

    }
}
