using MediatR;
using QwiikAppointmentService.Application.UseCases.AuthenticationUseCases.Common;

namespace QwiikAppointmentService.Application.UseCases.AuthenticationUseCases.Register
{
    public record RegisterUser(RegisterUserRequestType Request) : IRequest<UserResponseType>;
}
