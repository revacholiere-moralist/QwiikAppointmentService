namespace QwiikAppointmentService.Application.UseCases.AppointmentUseCases.GetAppointmentsByDate
{
    public class GetAppointmentsByDateRequestType
    {
        // Assume filter was given in UTC time from front end
        public DateTime AppointmentDateFilterStart { get; set; } = DateTime.MinValue;
        public DateTime AppointmentDateFilterEnd { get; set; } = DateTime.MaxValue;
        public int? CustomerId { get; set; }
    }
}
