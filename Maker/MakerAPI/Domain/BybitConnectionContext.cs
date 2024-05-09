using Bybit.Net.Clients;
using Bybit.Net.Enums;
using CryptoExchange.Net.Authentication;
using MakerAPI.Helpers;
using Microsoft.Extensions.Options;

namespace MakerAPI.Domain;

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

    public async Task<ApiCallResult<decimal>> GetAccountTotals()
    {
        var balancesResult = await _restClient.V5Api.Account.GetBalancesAsync(AccountType.Unified);
        if (balancesResult.Success)
        {
            var totals = balancesResult.Data.List.Sum(it => it.TotalAvailableBalance ?? 0);
            return ApiCallResult<decimal>.CreateOk(balancesResult.ResponseStatusCode.Value, totals);
        }

        return ApiCallResult<decimal>.CreateFailed(balancesResult.ResponseStatusCode.Value, balancesResult.Error?.Message);
    }
}