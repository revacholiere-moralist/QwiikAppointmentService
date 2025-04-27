using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using QwiikAppointmentService.Application.UseCases.AuthenticationUseCases.Login;
using QwiikAppointmentService.Application.UseCases.AuthenticationUseCases.Register;

namespace QwiikAppointmentService.WebAPI.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/authentication")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthenticationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterUserRequestType request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new RegisterUser(request), cancellationToken);
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginRequestType request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new Login(request), cancellationToken);
            return Ok(response);
        }

    }
}
