using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using QwiikAppointmentService.Domain.Entities;

namespace QwiikAppointmentService.EfPostgreSQL.Configurations
{
    public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.ToTable("Appointment");

            builder.HasKey(x => x.AppointmentId);
            
            builder.HasOne(x => x.Customer)
                .WithMany(x => x.Appointments)
                .HasForeignKey(x => x.CustomerId);

            builder.HasOne(x => x.CreatedBy)
                .WithMany()
                .HasForeignKey(x => x.CreatedById)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.LastUpdatedBy)
                .WithMany()
                .HasForeignKey(x => x.LastUpdatedById)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
