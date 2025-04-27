namespace QwiikAppointmentService.Application.UseCases.AuthenticationUseCases.Login
{
    public class LoginRequestType
    {
        public string? Email { get; set; }
        public string? Username { get; set; }
        public required string Password { get; set; }
    }
}
