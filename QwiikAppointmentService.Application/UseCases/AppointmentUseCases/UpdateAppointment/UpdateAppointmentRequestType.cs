namespace QwiikAppointmentService.Application.UseCases.AppointmentUseCases.UpdateAppointment
{
    public class UpdateAppointmentRequestType
    {
        public required int CustomerId { get; set; }
        public required int AppointmentId { get; set; }
        public required DateTime AppointmentStart { get; set; }
    }
}
