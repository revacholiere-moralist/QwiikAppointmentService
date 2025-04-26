namespace QwiikAppointmentService.Application.UseCases.AppointmentUseCases.CreateAppointment
{
    public class CreateAppointmentRequestType
    {
        public required int CustomerId { get; set; }
        public required DateTime AppointmentStart { get; set; }
    }
}
