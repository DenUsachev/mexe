using System.Net;
using System.Net.Mime;
using MakerAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace MakerAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Consumes(MediaTypeNames.Application.Json)]
[Produces(MediaTypeNames.Application.Json)]
public class PortfolioController : ControllerBase
{
    private readonly ILogger<PortfolioController> _logger;
    private readonly IBybitService _service;

    public PortfolioController(ILogger<PortfolioController> logger, IBybitService service)
    {
        _logger = logger;
        _service = service;
    }

    [HttpGet(Name = "GetAccountStatus")]
    public async Task<IActionResult> Portfolio()
    {
        _logger.LogInformation("Trying to get status of the portfolio");
        var portfolioInfoResult = await _service.GetPortfolioStatus();
        if (portfolioInfoResult.IsOk)
        {
            return Ok(portfolioInfoResult.Payload);
        }

        return StatusCode((int)HttpStatusCode.InternalServerError, portfolioInfoResult.Error);
    }
}