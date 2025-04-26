namespace QwiikAppointmentService.Application.Exceptions
{
    public class BadRequestException : Exception
    {
        public string[] Errors { get; set; }
        public BadRequestException(string message, string[] errors) : base(message)
        {
            Errors = errors;
        }
    }
}
