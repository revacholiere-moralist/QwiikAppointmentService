using QwiikAppointmentService.Domain.Entities;

namespace QwiikAppointmentService.Application.Repositories
{
    public interface IPublicHolidayRepository : IRepository<PublicHoliday>
    {
        Task<PublicHoliday?> GetByTime(DateTime dateTime, CancellationToken cancellationToken);  
    }

}
