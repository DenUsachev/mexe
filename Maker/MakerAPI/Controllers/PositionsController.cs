using MakerAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mime;
using MakerAPI.Domain;

namespace MakerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public class PositionController : ControllerBase
    {
        private readonly ILogger<PositionController> _logger;
        private readonly IExchangeService _service;

        public PositionController(ILogger<PositionController> logger, IExchangeService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet("list", Name = "Get open positions")]
        public async Task<IActionResult> GetPositions()
        {
            _logger.LogInformation("Trying to get open positions");
            var positionInfo = await _service.GetPositions();
            if (positionInfo.IsOk)
            {
                return Ok(new { Positions = positionInfo?.Payload?.ToList() });
            }
            _logger.LogError(positionInfo.Error);
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }
    }
}
