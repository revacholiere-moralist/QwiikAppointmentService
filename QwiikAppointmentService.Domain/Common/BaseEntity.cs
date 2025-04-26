using QwiikAppointmentService.Domain.Entities;

namespace QwiikAppointmentService.Domain.Common
{
    public abstract class BaseEntity
    {
        public required DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public required DateTime LastUpdatedDate { get; set; } = DateTime.UtcNow;
        public required bool IsActive { get; set; } = false;
    }
}
