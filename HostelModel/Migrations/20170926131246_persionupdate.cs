using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HostelModel.Migrations
{
    public partial class persionupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "T_Hostel_ServicePerson");

            migrationBuilder.AlterColumn<string>(
                name: "Pwd",
                table: "T_Hostel_ServicePerson",
                type: "varchar(250)",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 250);

            migrationBuilder.AlterColumn<string>(
                name: "IdentityCard",
                table: "T_Hostel_ServicePerson",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Pwd",
                table: "T_Hostel_ServicePerson",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(250)",
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IdentityCard",
                table: "T_Hostel_ServicePerson",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "T_Hostel_ServicePerson",
                maxLength: 25,
                nullable: false,
                defaultValue: "");
        }
    }
}
