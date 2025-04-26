using QwiikAppointmentService.Application.Repositories;
using QwiikAppointmentService.EfPostgreSQL.Context;

namespace QwiikAppointmentService.EfPostgreSQL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly QwiikAppointmentServiceDataContext _context;

        public UnitOfWork(QwiikAppointmentServiceDataContext context)
        {
            _context = context;
        }

        public Task Save(CancellationToken cancellationToken)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }
    }
}
