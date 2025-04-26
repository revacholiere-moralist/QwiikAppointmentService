using QwiikAppointmentService.Domain.Common;

namespace QwiikAppointmentService.Domain.Entities
{
    public class Appointment : BaseEntity
    {
        public required int AppointmentId { get; set; }
        public required int CustomerId { get; set; }
        public required Customer Customer { get; set; }
        public required DateTime AppointmentDateTimeStart { get; set; }
        public required DateTime AppointmentDateTimeEnd { get; set; }
        public required Person CreatedBy { get; set; }
        public int CreatedById { get; set; }
        public required Person LastUpdatedBy { get; set; }
        public int LastUpdatedById { get; set; }
    }
}
