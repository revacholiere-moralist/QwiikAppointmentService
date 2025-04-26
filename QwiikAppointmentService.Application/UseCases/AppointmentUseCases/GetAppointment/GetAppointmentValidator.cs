using FluentValidation;

namespace QwiikAppointmentService.Application.UseCases.AppointmentUseCases.GetAppointment
{
    public class GetAppointmentValidator : AbstractValidator<GetAppointment>
    {
        public GetAppointmentValidator()
        {
            RuleFor(x => x).NotNull().NotEmpty();
            RuleFor(x => x.CustomerId).NotEmpty().NotNull();
            RuleFor(x => x.AppointmentId).NotEmpty().NotNull();
        }
    }
}
