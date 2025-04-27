using QwiikAppointmentService.Domain.Entities;

namespace QwiikAppointmentService.Application.Repositories
{
    public interface IUserRepository
    {
        void Update(User user, CancellationToken cancellationToken);
    }

}
