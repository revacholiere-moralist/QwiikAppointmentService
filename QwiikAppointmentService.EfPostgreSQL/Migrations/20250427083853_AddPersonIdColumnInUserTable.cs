using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QwiikAppointmentService.EfPostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class AddPersonIdColumnInUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PersonId",
                table: "AspNetUsers",
                type: "integer",
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "AspNetUsers");
        }
    }
}
