namespace QwiikAppointmentService.Application.Exceptions
{
    public class UnauthorisedException : Exception
    {
        public string[] Errors { get; set; }
        public UnauthorisedException(string message, string[] errors) : base(message)
        {
            Errors = errors;
        }
    }
}
