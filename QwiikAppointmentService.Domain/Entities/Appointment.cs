using QwiikAppointmentService.Domain.Common;

namespace QwiikAppointmentService.Domain.Entities
{
    public class Appointment : BaseEntity
    {
        public int AppointmentId { get; set; }
        public required int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public required DateTime AppointmentDateTimeStart { get; set; }
        public required DateTime AppointmentDateTimeEnd { get; set; }
        public Person CreatedBy { get; set; }
        public int CreatedById { get; set; }
        public Person LastUpdatedBy { get; set; }
        public int LastUpdatedById { get; set; }
    }
}
