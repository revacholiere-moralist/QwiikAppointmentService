using FluentValidation;

namespace QwiikAppointmentService.Application.UseCases.AppointmentUseCases.DeleteAppointment
{
    public class DeleteAppointmentValidator : AbstractValidator<DeleteAppointment>
    {
        public DeleteAppointmentValidator()
        {
            RuleFor(x => x).NotNull().NotEmpty();
            RuleFor(x => x.CustomerId).NotEmpty().NotNull();
            RuleFor(x => x.AppointmentId).NotEmpty().NotNull();
        }
    }
}
