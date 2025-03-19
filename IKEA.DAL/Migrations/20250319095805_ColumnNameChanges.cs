using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IKEA.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ColumnNameChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastMofdifedBy",
                table: "Departments",
                newName: "LastModifiedBy");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "Departments",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastModifiedBy",
                table: "Departments",
                newName: "LastMofdifedBy");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "CreationDate",
                table: "Departments",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }
    }
}
