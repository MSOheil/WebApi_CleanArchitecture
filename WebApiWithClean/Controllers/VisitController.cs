using Application.Visits.Commands.CreateVisits;
using Application.Visits.Commands.DeleteVisits;
using Application.Visits.Commands.UpdateVisits;
using Application.Visits.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiWithClean.Controllers
{
    [ApiController]
    [Route("api/visitcontroller")]
    public class VisitController : ControllerBase
    {
        private readonly IMediator _mediator;
        public VisitController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IEnumerable<VisitReadDto>> GetVisitWithPagingAsync([FromQuery] GetVisitsWithPaginationQuery query)
        {
            return await _mediator.Send(query);
        }
        [HttpGet("{visitId}")]
        public async Task<VisitReadDto> GetVisitWithIdAsync([FromQuery] Guid visitId)
        {
            return await _mediator.Send(new GetVisitsWithIdQuery { VisitId = visitId });
        }
        [HttpPost]
        public async Task<IActionResult> CreateVisitAsync([FromBody] CreateVisitCommand query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        [HttpPut("visitId")]
        public async Task<IActionResult> UpdateVisittAsync([FromQuery] Guid visitId, [FromBody] UpdateVisitsCommand command)
        {
            if (visitId != command.VisitId)
            {
                return BadRequest();
            }
            await _mediator.Send(command);

            return Ok();
        }
        [HttpDelete("visitId")]
        public async Task<IActionResult> DeleteVisitAsync(Guid visitId)
        {
            await _mediator.Send(new DeleteVisitsCommand { VisitId = visitId });
            return Ok();
        }
    }
}
