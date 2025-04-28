using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace QwiikAppointmentService.EfPostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class AddPublicHolidayTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PublicHoliday",
                columns: table => new
                {
                    PublicHolidayId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    HolidayStart = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    HolidayEnd = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true),
                    LastUpdatedById = table.Column<int>(type: "integer", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublicHoliday", x => x.PublicHolidayId);
                    table.ForeignKey(
                        name: "FK_PublicHoliday_Person_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Person",
                        principalColumn: "PersonId");
                    table.ForeignKey(
                        name: "FK_PublicHoliday_Person_LastUpdatedById",
                        column: x => x.LastUpdatedById,
                        principalTable: "Person",
                        principalColumn: "PersonId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PublicHoliday_CreatedById",
                table: "PublicHoliday",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_PublicHoliday_LastUpdatedById",
                table: "PublicHoliday",
                column: "LastUpdatedById");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PublicHoliday");
        }
    }
}
