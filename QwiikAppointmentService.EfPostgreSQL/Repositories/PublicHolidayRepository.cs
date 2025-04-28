using Microsoft.EntityFrameworkCore;
using QwiikAppointmentService.Application.Repositories;
using QwiikAppointmentService.Domain.Entities;
using QwiikAppointmentService.EfPostgreSQL.Context;

namespace QwiikAppointmentService.EfPostgreSQL.Repositories
{
    public class PublicHolidayRepository : BaseRepository<PublicHoliday>, IPublicHolidayRepository
    {
        private readonly QwiikAppointmentServiceDataContext _context;
        public PublicHolidayRepository(
           QwiikAppointmentServiceDataContext context) : base(context)
        {
        }

        public async Task<PublicHoliday?> GetByTime(DateTime dateTime, CancellationToken cancellationToken)
        {
            return await Context.Query<PublicHoliday>()
                .Where(x => x.HolidayStart <= dateTime && x.HolidayEnd >= dateTime && x.IsActive)
                .FirstOrDefaultAsync(cancellationToken);

        }
    }
}
