using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using QwiikAppointmentService.Domain.Entities;
using System.Diagnostics.Metrics;

namespace QwiikAppointmentService.EfPostgreSQL.Configurations
{

    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("Person");

            builder.HasKey(x => x.PersonId);
            builder.HasOne(x => x.CreatedBy)
                .WithMany()
                .HasForeignKey(x => x.CreatedById)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.LastUpdatedBy)
                .WithMany()
                .HasForeignKey(x => x.LastUpdatedById)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasData(
                new Person {
                    PersonId = 1,
                    Username = "SuperAdmin",
                    Email = "admin@test.com",
                    FirstName = "SuperAdmin",
                    LastName = "SuperAdmin",
                    DateOfBirth = new DateTime(1745648363, DateTimeKind.Utc),
                    CreatedDate = new DateTime(1745648363, DateTimeKind.Utc),
                    LastUpdatedDate = new DateTime(1745648363, DateTimeKind.Utc),
                    IsActive = true,
                    CreatedById = null,
                    LastUpdatedById = null
                });

        }
    }
}
