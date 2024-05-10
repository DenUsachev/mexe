using Bybit.Net.Enums;

namespace MakerAPI.Domain;

public class PositionInfo
{
    public string Symbol { get; set; }
    public decimal Size { get; set; }
    public decimal Value { get; set; }
    public PositionStatus Status { get; set; }
}