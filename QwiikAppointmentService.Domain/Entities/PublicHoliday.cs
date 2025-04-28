using Microsoft.AspNetCore.Identity;
using QwiikAppointmentService.Domain.Common;

namespace QwiikAppointmentService.Domain.Entities
{
    public class PublicHoliday : BaseEntity
    {
        public int PublicHolidayId { get; set; }
        public DateTime HolidayStart { get; set; }
        public DateTime HolidayEnd { get; set; }
        public Person? CreatedBy { get; set; }
        public int? CreatedById { get; set; }
        public Person? LastUpdatedBy { get; set; }
        public int? LastUpdatedById { get; set; }
    }
}
