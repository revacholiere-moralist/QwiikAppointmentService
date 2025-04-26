using MediatR;
using QwiikAppointmentService.Application.UseCases.AppointmentUseCases.Common;

namespace QwiikAppointmentService.Application.UseCases.AppointmentUseCases.CreateAppointment
{
    public record CreateAppointment(CreateAppointmentRequestType Request) : IRequest<AppointmentResponseType>;
}
