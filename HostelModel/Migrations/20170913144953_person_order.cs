using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HostelModel.Migrations
{
    public partial class person_order : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Sex",
                table: "T_Hostel_ServicePerson",
                type: "varchar(25)",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25);

            migrationBuilder.CreateTable(
                name: "T_Hostel_PersonOrder",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    GUID = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false),
                    Mark = table.Column<string>(type: "varchar(1024)", maxLength: 1024, nullable: true),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Hostel_PersonOrder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_T_Hostel_PersonOrder_T_Hostel_WorkOrder_OrderId",
                        column: x => x.OrderId,
                        principalTable: "T_Hostel_WorkOrder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_T_Hostel_PersonOrder_T_Hostel_ServicePerson_PersonId",
                        column: x => x.PersonId,
                        principalTable: "T_Hostel_ServicePerson",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_T_Hostel_PersonOrder_OrderId",
                table: "T_Hostel_PersonOrder",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_T_Hostel_PersonOrder_PersonId",
                table: "T_Hostel_PersonOrder",
                column: "PersonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "T_Hostel_PersonOrder");

            migrationBuilder.AlterColumn<string>(
                name: "Sex",
                table: "T_Hostel_ServicePerson",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(25)",
                oldMaxLength: 25,
                oldNullable: true);
        }
    }
}
