using System.Net;
using MakerAPI.Domain;
using MakerAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace MakerAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    private readonly ILogger<AccountController> _logger;
    private readonly IBybitService _service;

    public AccountController(ILogger<AccountController> logger, IBybitService service)
    {
        _logger = logger;
        _service = service;
    }

    [HttpGet(Name = "GetAccountStatus")]
    public async Task<ActionResult> Status()
    {
        _logger.LogInformation("Trying to get status for active account");
        var balanceResult = await _service.GetAccountBalance();
        if (balanceResult.HttpStatusCode == HttpStatusCode.OK)
        {
            var accountStatus = new AccountStatus
            {
                Balance = balanceResult.Payload,
                Timestamp = DateTime.Now
            };
            return Ok(accountStatus);
        }

        return StatusCode((int)HttpStatusCode.InternalServerError);
    }
}