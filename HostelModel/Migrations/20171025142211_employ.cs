using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HostelModel.Migrations
{
    public partial class employ : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "KeyWord",
                table: "T_Hostel_WorkOrder",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Sort",
                table: "T_Hostel_Hotel",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PersonEmploys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Comment = table.Column<string>(type: "longtext", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Evaluate = table.Column<int>(type: "int", nullable: true),
                    GUID = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false),
                    HotelComment = table.Column<string>(type: "longtext", nullable: true),
                    HotelEvaluate = table.Column<int>(type: "int", nullable: true),
                    HotelOrderId = table.Column<int>(type: "int", nullable: false),
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonEmploys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonEmploys_T_Hostel_WorkOrder_HotelOrderId",
                        column: x => x.HotelOrderId,
                        principalTable: "T_Hostel_WorkOrder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonEmploys_T_Hostel_ServicePerson_PersonId",
                        column: x => x.PersonId,
                        principalTable: "T_Hostel_ServicePerson",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonEmploys_HotelOrderId",
                table: "PersonEmploys",
                column: "HotelOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonEmploys_PersonId",
                table: "PersonEmploys",
                column: "PersonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonEmploys");

            migrationBuilder.DropColumn(
                name: "KeyWord",
                table: "T_Hostel_WorkOrder");

            migrationBuilder.DropColumn(
                name: "Sort",
                table: "T_Hostel_Hotel");
        }
    }
}
