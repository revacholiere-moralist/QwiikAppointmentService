namespace QwiikAppointmentService.Application.UseCases.AuthenticationUseCases.Login
{
    public class TokenResponseType
    {
        public string AccessToken { get; set; }
        public string Type { get; set; }
        public DateTime AccessTokenExpires { get; set; }
        public string Username { get; set; }
        public string UserId { get; set; }

    }
}
