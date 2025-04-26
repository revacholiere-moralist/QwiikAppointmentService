namespace QwiikAppointmentService.Application.UseCases.AppointmentUseCases.Common
{
    public class AppointmentResponseType
    {
        public int AppointmentId { get; set; }
        public int CustomerId { get; set; }
        public DateTime AppointmentStartTime { get; set; }
        public DateTime AppointmentEndTime { get; set; }
    }
}
