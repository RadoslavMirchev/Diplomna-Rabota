using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Beauty_Salon.Migrations
{
    /// <inheritdoc />
    public partial class Testing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WorkerName",
                table: "Procedures");

            migrationBuilder.DropColumn(
                name: "ProcedureName",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "WorkerName",
                table: "Appointments");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "WorkerName",
                table: "Procedures",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProcedureName",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "WorkerName",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
