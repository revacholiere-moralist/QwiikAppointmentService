using FluentValidation;

namespace QwiikAppointmentService.Application.UseCases.AppointmentUseCases.UpdateAppointment
{
    public class UpdateAppointmentValidator : AbstractValidator<UpdateAppointment>
    {
        public UpdateAppointmentValidator()
        {
            RuleFor(x => x).NotNull().NotEmpty();
            RuleFor(x => x.Request.CustomerId).NotEmpty().NotNull();
            RuleFor(x => x.Request.AppointmentStart).NotEmpty().NotNull();
        }
    }
}
