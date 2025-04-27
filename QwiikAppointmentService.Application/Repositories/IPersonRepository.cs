using QwiikAppointmentService.Domain.Entities;

namespace QwiikAppointmentService.Application.Repositories
{
    public interface IPersonRepository : IRepository<Person>
    {
        Task<Person?> GetByUsername(string username, CancellationToken cancellationToken);  
    }

}
