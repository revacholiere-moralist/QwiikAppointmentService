using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QwiikAppointmentService.Domain.Entities;
using System.Xml.Linq;

namespace QwiikAppointmentService.EfPostgreSQL.Configurations
{

    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasData(
                new Role
                {
                    Id = "43b54c33-cd1b-4acf-ab03-30834d151562",
                    ConcurrencyStamp = "7c206ca4-fd12-4c39-94cf-bd7707f6fee4",
                    Name = "Customer",
                    NormalizedName = "CUSTOMER"
                },
                new Role
                {
                    Id = "429df4dd-9bb1-428b-95d5-7ed9069bdd0b",
                    ConcurrencyStamp = "a5529fd6-a373-43bf-b5d3-80e2b63156a9",
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new Role
                {
                    Id = "3a13be6f-c34e-4dfe-bfd4-2cc2d7a6b46f",
                    ConcurrencyStamp = "95dfc037-3d73-486f-9abb-fc97a390c9d9",
                    Name = "Agent",
                    NormalizedName = "AGENT"
                });

        }
    }
}
