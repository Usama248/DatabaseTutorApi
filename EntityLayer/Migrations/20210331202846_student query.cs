using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EntityLayer.Migrations
{
    public partial class studentquery : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Database_Tutor_Student_Query",
                columns: table => new
                {
                    dtsq_key = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    dtsq_student_id = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    dtsq_query_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dtsq_query = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Database_Tutor_Student_Query", x => x.dtsq_key);
                    table.ForeignKey(
                        name: "FK_Database_Tutor_Student_Query_AspNetUsers_dtsq_student_id",
                        column: x => x.dtsq_student_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Database_Tutor_Student_Query_dtsq_student_id",
                table: "Database_Tutor_Student_Query",
                column: "dtsq_student_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Database_Tutor_Student_Query");
        }
    }
}
