using MediatR;
using QwiikAppointmentService.Application.UseCases.AppointmentUseCases.Common;

namespace QwiikAppointmentService.Application.UseCases.AppointmentUseCases.GetAppointment
{
    public record GetAppointment(int AppointmentId, int CustomerId, bool IsFromCustomer = false) : IRequest<AppointmentResponseType>;
}
