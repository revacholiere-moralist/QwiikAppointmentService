namespace QwiikAppointmentService.Application.UseCases.AuthenticationUseCases.Common
{
    public class UserResponseType
    {
        public required string UserId { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string Username { get; set; }
        public required int PersonId { get; set; }
    }
}
