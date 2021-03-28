using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EntityLayer.Migrations
{
    public partial class addassignmenttTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Database_Tutor_Class",
                columns: table => new
                {
                    dt_key = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    dt_name = table.Column<string>(type: "varchar(200)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Database_Tutor_Class", x => x.dt_key);
                });

            migrationBuilder.CreateTable(
                name: "Database_Tutor_Assignment",
                columns: table => new
                {
                    dta_key = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    dta_title = table.Column<string>(type: "varchar(200)", nullable: false),
                    dta_class_id = table.Column<int>(type: "int", nullable: false),
                    dta_file_location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Database_Tutor_Assignment", x => x.dta_key);
                    table.ForeignKey(
                        name: "FK_Database_Tutor_Assignment_Database_Tutor_Class_dta_class_id",
                        column: x => x.dta_class_id,
                        principalTable: "Database_Tutor_Class",
                        principalColumn: "dt_key",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Database_Tutor_Assignment_Solution",
                columns: table => new
                {
                    dtas_key = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    dtas_user_id = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    dtas_assignment_id = table.Column<int>(type: "int", nullable: false),
                    dtas_file_location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Database_Tutor_Assignment_Solution", x => x.dtas_key);
                    table.ForeignKey(
                        name: "FK_Database_Tutor_Assignment_Solution_AspNetUsers_dtas_user_id",
                        column: x => x.dtas_user_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Database_Tutor_Assignment_Solution_Database_Tutor_Assignment_dtas_assignment_id",
                        column: x => x.dtas_assignment_id,
                        principalTable: "Database_Tutor_Assignment",
                        principalColumn: "dta_key",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Database_Tutor_Assignment_dta_class_id",
                table: "Database_Tutor_Assignment",
                column: "dta_class_id");

            migrationBuilder.CreateIndex(
                name: "IX_Database_Tutor_Assignment_Solution_dtas_assignment_id",
                table: "Database_Tutor_Assignment_Solution",
                column: "dtas_assignment_id");

            migrationBuilder.CreateIndex(
                name: "IX_Database_Tutor_Assignment_Solution_dtas_user_id",
                table: "Database_Tutor_Assignment_Solution",
                column: "dtas_user_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Database_Tutor_Assignment_Solution");

            migrationBuilder.DropTable(
                name: "Database_Tutor_Assignment");

            migrationBuilder.DropTable(
                name: "Database_Tutor_Class");
        }
    }
}
