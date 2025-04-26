using MediatR;
using QwiikAppointmentService.Application.UseCases.AppointmentUseCases.Common;

namespace QwiikAppointmentService.Application.UseCases.AppointmentUseCases.UpdateAppointment
{
    public record UpdateAppointment(UpdateAppointmentRequestType Request) : IRequest<AppointmentResponseType>;
}
