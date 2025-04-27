using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QwiikAppointmentService.Application.UseCases.AppointmentUseCases.CreateAppointment;
using QwiikAppointmentService.Application.UseCases.AppointmentUseCases.DeleteAppointment;
using QwiikAppointmentService.Application.UseCases.AppointmentUseCases.GetAppointment;
using QwiikAppointmentService.Application.UseCases.AppointmentUseCases.GetAppointmentsByDate;
using QwiikAppointmentService.Application.UseCases.AppointmentUseCases.UpdateAppointment;
using QwiikAppointmentService.Domain.Constants;
using QwiikAppointmentService.Domain.Entities;
using System.Security.Claims;

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

        [Authorize]
        [HttpGet("{appointmentId:int}/customer/{customerId:int}")]
        public async Task<IActionResult> GetAppointmentById(int appointmentId, int customerId, CancellationToken cancellationToken)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                if (claims.Any(x => x.Value == "Customer" && x.Type == ClaimType.Role))
                {
                    customerId = int.Parse(claims.FirstOrDefault(x => x.Type == ClaimType.PersonId).Value);
                }
            }
            var response = await _mediator.Send(new GetAppointment(appointmentId, customerId), cancellationToken);
            return Ok(response);
        }

        [Authorize]
        [HttpPost("filter-by-date")]
        public async Task<IActionResult> GetAppointmentByDate(GetAppointmentsByDateRequestType request, CancellationToken cancellationToken)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                if (claims.Any(x => x.Value == "Customer" && x.Type == ClaimType.Role))
                {
                    request.CustomerId = int.Parse(claims.FirstOrDefault(x => x.Type == ClaimType.PersonId).Value);
                }
            }

            var response = await _mediator.Send(new GetAppointmentsByDate(request), cancellationToken);
            return Ok(response);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateAppointment(CreateAppointmentRequestType request, CancellationToken cancellationToken)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                if (claims.Any(x => x.Value == "Customer" && x.Type == ClaimType.Role))
                {
                    var customerId = int.Parse(claims.FirstOrDefault(x => x.Type == ClaimType.PersonId).Value);
                    if (customerId != request.CustomerId)
                    {
                        return Unauthorized("You are not authorized to create an appointment for this customer.");
                    }
                }
            }
            var response = await _mediator.Send(new CreateAppointment(request), cancellationToken);
            return Ok(response);
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateAppointment(UpdateAppointmentRequestType request, CancellationToken cancellationToken)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                if (claims.Any(x => x.Value == "Customer" && x.Type == ClaimType.Role))
                {
                    var customerId = int.Parse(claims.FirstOrDefault(x => x.Type == ClaimType.PersonId).Value);
                    if (customerId != request.CustomerId)
                    {
                        return Unauthorized("You are not authorized to update an appointment for this customer.");
                    }
                }
            }
            var response = await _mediator.Send(new UpdateAppointment(request), cancellationToken);
            return Ok(response);
        }

        [Authorize]
        [HttpDelete("{appointmentId:int}/customer/{customerId:int}")]
        public async Task<IActionResult> DeleteAppointment(int appointmentId, int customerId, CancellationToken cancellationToken)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                if (claims.Any(x => x.Value == "Customer" && x.Type == ClaimType.Role))
                {
                    var loggedInCustomerId = int.Parse(claims.FirstOrDefault(x => x.Type == ClaimType.PersonId).Value);

                    if (customerId != loggedInCustomerId)
                    {
                        return Unauthorized("You are not authorized to delete an appointment for this customer.");
                    }
                }
            }
            await _mediator.Send(new DeleteAppointment(appointmentId, customerId), cancellationToken);
            return Ok();
        }

    }
}
