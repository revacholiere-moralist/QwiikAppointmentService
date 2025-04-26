using FluentValidation;

namespace QwiikAppointmentService.Application.UseCases.AppointmentUseCases.GetAppointmentsByDate
{
    public class GetAppointmentsByDateValidator : AbstractValidator<GetAppointmentsByDate>
    {
        public GetAppointmentsByDateValidator()
        {
            RuleFor(x => x).NotNull().NotEmpty();
        }
    }
}
