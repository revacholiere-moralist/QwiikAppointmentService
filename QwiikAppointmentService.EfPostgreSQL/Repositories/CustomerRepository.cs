using QwiikAppointmentService.Application.Repositories;
using QwiikAppointmentService.Domain.Entities;
using QwiikAppointmentService.EfPostgreSQL.Context;

namespace QwiikAppointmentService.EfPostgreSQL.Repositories
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        private readonly QwiikAppointmentServiceDataContext _context;
        public CustomerRepository(
           QwiikAppointmentServiceDataContext context) : base(context)
        {
        }
    }
}
