namespace QwiikAppointmentService.Application.UseCases.AuthenticationUseCases.Login
{
    public class TokenResponseType
    {
        public required string AccessToken { get; set; }
        public required string Type { get; set; }
        public required DateTime AccessTokenExpires { get; set; }
        public required string Username { get; set; }
        public required string UserId { get; set; }
        public required int PersonId { get; set; }

    }
}
