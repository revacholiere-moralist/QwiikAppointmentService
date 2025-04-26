using QwiikAppointmentService.Domain.Common;

namespace QwiikAppointmentService.Application.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<List<T>> GetAll(CancellationToken cancellationToken);
        Task<T> Get(int id, CancellationToken cancellationToken);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
