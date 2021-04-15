using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EntityLayer.Migrations
{
    public partial class StudentQueryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Database_Tutor_StudentClass");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Database_Tutor_StudentClass");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Database_Tutor_StudentClass");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Database_Tutor_StudentClass");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "Database_Tutor_StudentClass");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Database_Tutor_StudentClass",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Database_Tutor_StudentClass",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Database_Tutor_StudentClass",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "Database_Tutor_StudentClass",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "Database_Tutor_StudentClass",
                type: "datetime2",
                nullable: true);
        }
    }
}
