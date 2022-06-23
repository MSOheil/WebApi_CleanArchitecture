using Application.Patients.Commands;
using Application.Patients.Commands.CreatePatient;
using Application.Patients.Commands.UpdatePatient;
using Application.Patients.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiWithClean.Controllers
{
    [ApiController]
    [Route("api/patientcontroller")]
    public class PatientController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PatientController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IEnumerable<ReadPatientDto>> GetPatientWithPagingAsync([FromQuery] GetPatientsWithPaginationQuery query)
        {
           return await _mediator.Send(query);
        }
        [HttpGet("{patientId}")]
        public async Task<ReadPatientWithIdDto> GetPatientWithIdAsync([FromQuery] Guid patientId)
        {
            return await _mediator.Send(new GetPatientsWithIdQuery { PatienId = patientId});
        }
        [HttpPost]
        public async Task<IActionResult> CreatePatientAsync([FromBody] CreatePatientCommand query)
        {
            var result= await _mediator.Send(query);
            return Ok(result);
        }
        [HttpPut("patientid")]
        public async Task<IActionResult> UpdatePatientAsync([FromQuery]Guid patientId,[FromBody] UpdatePatientCommand command)
        {
            if(patientId != command.PatientId)
            {
                return BadRequest();
            }
            await _mediator.Send(command);

            return Ok();
        }
        [HttpDelete("patientId")]
        public async Task<IActionResult> DeletePatientAsync(Guid patientId)
        {
            await _mediator.Send(new DeletePatientCommand { PatientId = patientId });
            return Ok();
        }

    }
}
