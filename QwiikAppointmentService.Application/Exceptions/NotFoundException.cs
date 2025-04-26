namespace QwiikAppointmentService.Application.Exceptions
{
    public class NotFoundException : Exception
    {
        public string[] Errors { get; set; }
        public NotFoundException(string message) : base(message)
        {
        }
        public NotFoundException(string message, string[] errors) : base(message)
        {
            Errors = errors;
        }
    }
}
