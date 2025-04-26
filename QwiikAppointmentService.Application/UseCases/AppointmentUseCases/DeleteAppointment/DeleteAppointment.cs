using MediatR;

namespace QwiikAppointmentService.Application.UseCases.AppointmentUseCases.DeleteAppointment
{
    public record DeleteAppointment(int AppointmentId, int CustomerId) : IRequest;
}
