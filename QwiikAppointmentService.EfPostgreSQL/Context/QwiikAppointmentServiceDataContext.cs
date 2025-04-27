using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QwiikAppointmentService.Domain.Common;
using QwiikAppointmentService.Domain.Entities;
using System.Reflection;

namespace QwiikAppointmentService.EfPostgreSQL.Context
{
    public class QwiikAppointmentServiceDataContext : IdentityDbContext
    {
        public QwiikAppointmentServiceDataContext(DbContextOptions<QwiikAppointmentServiceDataContext> options) : base(options)
        {
        }

        public DbSet<Person> Person { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<User> User { get; set; }

        public IQueryable<TEntity> Query<TEntity>() where TEntity : BaseEntity
        {
            return Set<TEntity>().AsNoTracking();
        }

        public TEntity Get<TEntity>(int id) where TEntity : BaseEntity
        {
            return Set<TEntity>().Find(id);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
