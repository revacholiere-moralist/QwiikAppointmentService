﻿namespace QwiikAppointmentService.Application.UseCases.AuthenticationUseCases.Register
{
    public class RegisterUserRequestType
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Password { get; set; }
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required DateTime DateOfBirth { get; set; }
    }
}
