namespace QwiikAppointmentService.Domain.Entities
{
    public class Customer : Person
    {
        public required DateTime RegistrationDate { get; set; } = DateTime.UtcNow;
        public List<Appointment> Appointments { get; set; } = new List<Appointment>();


    }
}
