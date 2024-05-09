namespace MakerAPI.Domain;

public class AccountStatus
{
    public decimal Balance { get; set; }
    public DateTime Timestamp { get; set; }
    public string OpenPositions { get; set; }
}