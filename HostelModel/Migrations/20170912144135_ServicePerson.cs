using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HostelModel.Migrations
{
    public partial class ServicePerson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HotelName",
                table: "T_Hostel_WorkOrder");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HotelName",
                table: "T_Hostel_WorkOrder",
                nullable: true);
        }
    }
}
