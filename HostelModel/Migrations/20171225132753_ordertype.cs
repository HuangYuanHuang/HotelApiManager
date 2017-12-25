using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HostelModel.Migrations
{
    public partial class ordertype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Max",
                table: "T_Hostel_WorkOrder",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Min",
                table: "T_Hostel_WorkOrder",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrderType",
                table: "T_Hostel_WorkOrder",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Max",
                table: "T_Hostel_WorkOrder");

            migrationBuilder.DropColumn(
                name: "Min",
                table: "T_Hostel_WorkOrder");

            migrationBuilder.DropColumn(
                name: "OrderType",
                table: "T_Hostel_WorkOrder");
        }
    }
}
