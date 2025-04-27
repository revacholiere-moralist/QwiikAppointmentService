using QwiikAppointmentService.Application.Repositories;
using QwiikAppointmentService.Domain.Entities;
using QwiikAppointmentService.EfPostgreSQL.Context;

namespace QwiikAppointmentService.EfPostgreSQL.Repositories
{
    public class PersonRepository : BaseRepository<Person>, IPersonRepository
    {
        private readonly QwiikAppointmentServiceDataContext _context;
        public PersonRepository(
           QwiikAppointmentServiceDataContext context) : base(context)
        {
        }
    }
}
