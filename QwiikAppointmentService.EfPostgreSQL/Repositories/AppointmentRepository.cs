using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using QwiikAppointmentService.Application.Repositories;
using QwiikAppointmentService.Domain.Entities;
using QwiikAppointmentService.EfPostgreSQL.Context;

namespace QwiikAppointmentService.EfPostgreSQL.Repositories
{
    public class AppointmentRepository : BaseRepository<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(
           QwiikAppointmentServiceDataContext context) : base(context)
        {
        }

        public async Task<Appointment?> GetAppointmentByStartTime(DateTime appointmentStartTime, CancellationToken cancellationToken)
        {
            return await Context.Query<Appointment>()
                .Where(x => x.AppointmentDateTimeStart <= appointmentStartTime
                            && x.AppointmentDateTimeEnd > appointmentStartTime
                            && x.IsActive)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<List<Appointment>> GetAppointmentsByDate(DateTime dateFilterStart, DateTime dateFilterEnd, CancellationToken cancellationToken)
        {
            return await Context.Query<Appointment>()
                .Where(x => x.IsActive 
                            && (x.AppointmentDateTimeStart >= dateFilterStart && x.AppointmentDateTimeStart <= dateFilterEnd
                                || x.AppointmentDateTimeEnd >= dateFilterStart && x.AppointmentDateTimeEnd <= dateFilterEnd))
                .ToListAsync(cancellationToken);
        }
    }
}
