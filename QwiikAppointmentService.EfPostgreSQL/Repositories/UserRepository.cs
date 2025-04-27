using Microsoft.EntityFrameworkCore;
using QwiikAppointmentService.Application.Repositories;
using QwiikAppointmentService.Domain.Entities;
using QwiikAppointmentService.EfPostgreSQL.Context;

namespace QwiikAppointmentService.EfPostgreSQL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly QwiikAppointmentServiceDataContext _context;
        public UserRepository(
           QwiikAppointmentServiceDataContext context)
        {
            _context = context;
        }

        public void Update(User user, CancellationToken cancellationToken)
        {
            _context.Update(user);
        }
    }
}
