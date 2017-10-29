using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HostelModel.Migrations
{
    public partial class HotelWorkOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "T_Hostel_WorkOrder",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Billing = table.Column<string>(type: "longtext", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    End = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    GUID = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false),
                    HotelId = table.Column<int>(type: "int", nullable: false),
                    HotelName = table.Column<string>(type: "longtext", nullable: true),
                    Mark = table.Column<string>(type: "longtext", nullable: true),
                    Num = table.Column<int>(type: "int", nullable: false),
                    ScheduleId = table.Column<int>(type: "int", nullable: false),
                    Start = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Title = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false),
                    WorkTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Hostel_WorkOrder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_T_Hostel_WorkOrder_T_Hostel_Hotel_HotelId",
                        column: x => x.HotelId,
                        principalTable: "T_Hostel_Hotel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_T_Hostel_WorkOrder_T_Hostel_Schedule_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "T_Hostel_Schedule",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_T_Hostel_WorkOrder_T_Hostel_WorkType_WorkTypeId",
                        column: x => x.WorkTypeId,
                        principalTable: "T_Hostel_WorkType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_T_Hostel_WorkOrder_HotelId",
                table: "T_Hostel_WorkOrder",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_T_Hostel_WorkOrder_ScheduleId",
                table: "T_Hostel_WorkOrder",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_T_Hostel_WorkOrder_WorkTypeId",
                table: "T_Hostel_WorkOrder",
                column: "WorkTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "T_Hostel_WorkOrder");
        }
    }
}
