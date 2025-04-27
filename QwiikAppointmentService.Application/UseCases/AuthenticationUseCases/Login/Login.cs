using MediatR;
using QwiikAppointmentService.Application.UseCases.AuthenticationUseCases.Common;

namespace QwiikAppointmentService.Application.UseCases.AuthenticationUseCases.Login
{
    public record Login(LoginRequestType Request) : IRequest<TokenResponseType>;
}
