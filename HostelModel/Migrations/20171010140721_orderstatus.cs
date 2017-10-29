using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HostelModel.Migrations
{
    public partial class orderstatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Examine",
                table: "T_Hostel_WorkOrder",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "T_Hostel_WorkOrder",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Examine",
                table: "T_Hostel_WorkOrder");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "T_Hostel_WorkOrder");
        }
    }
}
