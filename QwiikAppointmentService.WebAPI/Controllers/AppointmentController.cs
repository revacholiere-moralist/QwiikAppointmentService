using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using QwiikAppointmentService.Application.UseCases.AppointmentUseCases.CreateAppointment;
using QwiikAppointmentService.Application.UseCases.AppointmentUseCases.DeleteAppointment;
using QwiikAppointmentService.Application.UseCases.AppointmentUseCases.GetAppointment;
using QwiikAppointmentService.Application.UseCases.AppointmentUseCases.GetAppointmentsByDate;
using QwiikAppointmentService.Application.UseCases.AppointmentUseCases.UpdateAppointment;

namespace QwiikAppointmentService.WebAPI.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/appointment")]
    public class AppointmentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AppointmentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{appointmentId:int}/customer/{customerId:int}")]

        public async Task<IActionResult> GetAppointmentById(int appointmentId, int customerId, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetAppointment(appointmentId, customerId), cancellationToken);
            return Ok(response);
        }


        [HttpPost("filter-by-date")]

        public async Task<IActionResult> GetAppointmentByDate(GetAppointmentsByDateRequestType request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetAppointmentsByDate(request), cancellationToken);
            return Ok(response);
        }

        [HttpPost]

        public async Task<IActionResult> CreateAppointment(CreateAppointmentRequestType request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new CreateAppointment(request), cancellationToken);
            return Ok(response);
        }


        [HttpPut]

        public async Task<IActionResult> UpdateAppointment(UpdateAppointmentRequestType request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new UpdateAppointment(request), cancellationToken);
            return Ok(response);
        }

        [HttpDelete("{appointmentId:int}/customer/{customerId:int}")]

        public async Task<IActionResult> DeleteAppointment(int appointmentId, int customerId, CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeleteAppointment(appointmentId, customerId), cancellationToken);
            return Ok();
        }

    }
}
