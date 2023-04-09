﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Beauty_Salon.Data.Migrations
{
    /// <inheritdoc />
    public partial class AppointmentsFixed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AppointmentDuration",
                table: "Appointments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "LocationAddress",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ProcedureId",
                table: "Appointments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_ProcedureId",
                table: "Appointments",
                column: "ProcedureId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Procedures_ProcedureId",
                table: "Appointments",
                column: "ProcedureId",
                principalTable: "Procedures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Procedures_ProcedureId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_ProcedureId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "AppointmentDuration",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "LocationAddress",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "ProcedureId",
                table: "Appointments");
        }
    }
}
