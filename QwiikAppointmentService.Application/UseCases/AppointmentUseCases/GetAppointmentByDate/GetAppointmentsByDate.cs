using MediatR;
using QwiikAppointmentService.Application.UseCases.AppointmentUseCases.Common;

namespace QwiikAppointmentService.Application.UseCases.AppointmentUseCases.GetAppointmentsByDate
{
    public record GetAppointmentsByDate(GetAppointmentsByDateRequestType Request, bool IsFromCustomer = false) : IRequest<List<AppointmentResponseType>>;
}
