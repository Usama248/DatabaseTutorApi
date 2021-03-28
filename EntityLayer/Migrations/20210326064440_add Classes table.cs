using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EntityLayer.Migrations
{
    public partial class addClassestable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Database_Tutor_Assignment_Database_Tutor_Class_dta_class_id",
                table: "Database_Tutor_Assignment");

            migrationBuilder.RenameColumn(
                name: "dt_name",
                table: "Database_Tutor_Class",
                newName: "dtc_name");

            migrationBuilder.RenameColumn(
                name: "dt_key",
                table: "Database_Tutor_Class",
                newName: "dtc_key");

            migrationBuilder.CreateTable(
                name: "Database_Tutor_StudentClass",
                columns: table => new
                {
                    dtsc_key = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    dtsc_student_id = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    dtsc_class_id = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Database_Tutor_StudentClass", x => x.dtsc_key);
                    table.ForeignKey(
                        name: "FK_Database_Tutor_StudentClass_AspNetUsers_dtsc_student_id",
                        column: x => x.dtsc_student_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Database_Tutor_StudentClass_Database_Tutor_Class_dtsc_class_id",
                        column: x => x.dtsc_class_id,
                        principalTable: "Database_Tutor_Class",
                        principalColumn: "dtc_key",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Database_Tutor_TeacherClass",
                columns: table => new
                {
                    dttc_key = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    dttc_teacher_id = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    dttc_class_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Database_Tutor_TeacherClass", x => x.dttc_key);
                    table.ForeignKey(
                        name: "FK_Database_Tutor_TeacherClass_AspNetUsers_dttc_teacher_id",
                        column: x => x.dttc_teacher_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Database_Tutor_TeacherClass_Database_Tutor_Class_dttc_class_id",
                        column: x => x.dttc_class_id,
                        principalTable: "Database_Tutor_Class",
                        principalColumn: "dtc_key",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Database_Tutor_StudentClass_dtsc_class_id",
                table: "Database_Tutor_StudentClass",
                column: "dtsc_class_id");

            migrationBuilder.CreateIndex(
                name: "IX_Database_Tutor_StudentClass_dtsc_student_id",
                table: "Database_Tutor_StudentClass",
                column: "dtsc_student_id",
                unique: true,
                filter: "[dtsc_student_id] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Database_Tutor_TeacherClass_dttc_class_id",
                table: "Database_Tutor_TeacherClass",
                column: "dttc_class_id");

            migrationBuilder.CreateIndex(
                name: "IX_Database_Tutor_TeacherClass_dttc_teacher_id",
                table: "Database_Tutor_TeacherClass",
                column: "dttc_teacher_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Database_Tutor_Assignment_Database_Tutor_StudentClass_dta_class_id",
                table: "Database_Tutor_Assignment",
                column: "dta_class_id",
                principalTable: "Database_Tutor_StudentClass",
                principalColumn: "dtsc_key",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Database_Tutor_Assignment_Database_Tutor_StudentClass_dta_class_id",
                table: "Database_Tutor_Assignment");

            migrationBuilder.DropTable(
                name: "Database_Tutor_StudentClass");

            migrationBuilder.DropTable(
                name: "Database_Tutor_TeacherClass");

            migrationBuilder.RenameColumn(
                name: "dtc_name",
                table: "Database_Tutor_Class",
                newName: "dt_name");

            migrationBuilder.RenameColumn(
                name: "dtc_key",
                table: "Database_Tutor_Class",
                newName: "dt_key");

            migrationBuilder.AddForeignKey(
                name: "FK_Database_Tutor_Assignment_Database_Tutor_Class_dta_class_id",
                table: "Database_Tutor_Assignment",
                column: "dta_class_id",
                principalTable: "Database_Tutor_Class",
                principalColumn: "dt_key",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
