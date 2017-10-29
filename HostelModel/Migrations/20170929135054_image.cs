using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HostelModel.Migrations
{
    public partial class image : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Health",
                table: "T_Hostel_ServicePerson",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ICardBack",
                table: "T_Hostel_ServicePerson",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ICardPositive",
                table: "T_Hostel_ServicePerson",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Latitude",
                table: "T_Hostel_Hotel",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Longitude",
                table: "T_Hostel_Hotel",
                type: "longtext",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Health",
                table: "T_Hostel_ServicePerson");

            migrationBuilder.DropColumn(
                name: "ICardBack",
                table: "T_Hostel_ServicePerson");

            migrationBuilder.DropColumn(
                name: "ICardPositive",
                table: "T_Hostel_ServicePerson");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "T_Hostel_Hotel");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "T_Hostel_Hotel");
        }
    }
}
