namespace QwiikAppointmentService.Application.Exceptions
{
    public class InternalServerErrorException : Exception
    {
        public string[] Errors { get; set; }
        public InternalServerErrorException(string message) : base(message)
        {
        }
        public InternalServerErrorException(string message, string[] errors) : base(message)
        {
            Errors = errors;
        }
    }
}
