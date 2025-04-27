using QwiikAppointmentService.Domain.Entities;

namespace QwiikAppointmentService.Application.Repositories
{
    public interface IAppointmentRepository : IRepository<Appointment>
    {
        Task<Appointment?> GetAppointmentByStartTime(DateTime appointmentStartTime, CancellationToken cancellationToken);
        Task<List<Appointment>> GetAppointmentsByDate(DateTime dateFilterStart, DateTime dateFilterEnd, CancellationToken cancellationToken);
    }

}
