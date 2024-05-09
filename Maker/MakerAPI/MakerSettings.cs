namespace MakerAPI;

public class MakerSettings
{
    public WarehouseSettings Warehouse { get; set; }
    public ExchangeSettings Exchange { get; set; }
}

public class WarehouseSettings
{
    public string Uri { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
}

public class ExchangeSettings
{
    public string Uri { get; set; }
    public string Token { get; set; }
    public string Secret { get; set; }
}