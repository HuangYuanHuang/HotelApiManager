using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HostelModel.Migrations
{
    public partial class depart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "T_Hostel_WorkOrder");

            migrationBuilder.AddColumn<int>(
                name: "DepartID",
                table: "T_Hostel_WorkOrder",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "T_Hostel_Department",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DepartmentName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Duty = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false),
                    GUID = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false),
                    Mark = table.Column<string>(type: "varchar(1024)", maxLength: 1024, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Hostel_Department", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_T_Hostel_WorkOrder_DepartID",
                table: "T_Hostel_WorkOrder",
                column: "DepartID");

            migrationBuilder.AddForeignKey(
                name: "FK_T_Hostel_WorkOrder_T_Hostel_Department_DepartID",
                table: "T_Hostel_WorkOrder",
                column: "DepartID",
                principalTable: "T_Hostel_Department",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_T_Hostel_WorkOrder_T_Hostel_Department_DepartID",
                table: "T_Hostel_WorkOrder");

            migrationBuilder.DropTable(
                name: "T_Hostel_Department");

            migrationBuilder.DropIndex(
                name: "IX_T_Hostel_WorkOrder_DepartID",
                table: "T_Hostel_WorkOrder");

            migrationBuilder.DropColumn(
                name: "DepartID",
                table: "T_Hostel_WorkOrder");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "T_Hostel_WorkOrder",
                maxLength: 250,
                nullable: false,
                defaultValue: "");
        }
    }
}
