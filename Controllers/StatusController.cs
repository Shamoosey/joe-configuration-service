using Joebot_Backend.DTOs;
using Joebot_Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Joebot_Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StatusController : ControllerBase
    {
        private readonly ILogger<StatusController> _logger;
        private readonly IStatusService _statusService;

        public StatusController(ILogger<StatusController> logger, IStatusService statusService)
        {
            _logger = logger;
            _statusService = statusService;
        }

        [HttpGet]
        public async Task<List<StatusMessageDTO>> GetConfiguration(CancellationToken cancellationToken)
        {
            return await _statusService.GetStatusMessages();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteStatus(Guid id, CancellationToken cancellationToken)
        {
            var result = await this._statusService.DeleteStatusMessage(id);

            if(result)
            {
                return Ok();
            } 
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateStatusMessage(string statusMessage, int statusType, CancellationToken cancellationToken)
        {
            var result = await this._statusService.CreateStatusMessage(statusMessage, statusType);

            if(result)
            {
                return Ok();
            } 
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}