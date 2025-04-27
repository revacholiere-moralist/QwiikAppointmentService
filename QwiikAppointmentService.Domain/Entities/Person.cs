using QwiikAppointmentService.Domain.Common;

namespace QwiikAppointmentService.Domain.Entities
{
    public class Person : BaseEntity
    {
        public int PersonId { get; set; }
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required DateTime DateOfBirth { get; set; }
        public Person? CreatedBy { get; set; }
        public int? CreatedById { get; set; }
        public Person? LastUpdatedBy { get; set; }
        public int? LastUpdatedById { get; set; }

    }
}
