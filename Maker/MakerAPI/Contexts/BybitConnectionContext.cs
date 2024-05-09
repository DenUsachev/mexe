using Bybit.Net.Clients;
using Bybit.Net.Enums;
using CryptoExchange.Net.Authentication;
using MakerAPI.Domain;
using MakerAPI.Helpers;
using Microsoft.Extensions.Options;

namespace MakerAPI.Contexts;

public class BybitConnectionContext : IConnectionContext
{
    private readonly IOptions<MakerSettings> _applicationSettings;
    private BybitRestClient _restClient;

    public BybitConnectionContext(IOptions<MakerSettings> applicationSettings)
    {
        _applicationSettings = applicationSettings;
    }

    public bool OpenContext()
    {
        try
        {
            if (_applicationSettings?.Value?.Exchange is { Token: not null, Secret: not null })
            {
                _restClient = new BybitRestClient(options =>
                {
                    options.ApiCredentials = new ApiCredentials(_applicationSettings.Value.Exchange.Token, _applicationSettings.Value.Exchange.Secret);
                });
            }
        }
        catch (ArgumentException)
        {
            return false;
        }

        return true;
    }

    public async Task<ApiCallResult<PortfolioInfo>> GetPortfolioInfo()
    {
        var balancesResult = await _restClient.V5Api.Account.GetAllAssetBalancesAsync(AccountType.Unified);
        var totalsResult = await _restClient.V5Api.Account.GetBalancesAsync(AccountType.Unified);

        if (balancesResult.Success && totalsResult.Success)
        {
            var assetTotals = balancesResult.Data.Balances.Where(it => it.WalletBalance is > 0);
            var totals = totalsResult.Data.List.Sum(it => it.TotalWalletBalance);
            return ApiCallResult<PortfolioInfo>.CreateOk(new PortfolioInfo
            {
                Assets = assetTotals,
                UsdTotal = totals ?? 0
            });
        }

        return ApiCallResult<PortfolioInfo>.CreateFailed(balancesResult.Error?.Message);
    }

    public async Task<ApiCallResult<IEnumerable<PositionInfo>>> GetOpenPositions()
    {
        // todo: fix stub
        return ApiCallResult<IEnumerable<PositionInfo>>.CreateOk(new List<PositionInfo>
        {
            new()
            {
                Status = PositionStatus.Normal,
                Value = 1,
                Size = 1,
                Symbol = "STUB"
            }});
    }
}