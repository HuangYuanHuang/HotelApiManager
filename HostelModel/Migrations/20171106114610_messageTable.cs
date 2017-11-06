using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HostelModel.Migrations
{
    public partial class messageTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Messages",
                table: "Messages");

            migrationBuilder.RenameTable(
                name: "Messages",
                newName: "T_Hostel_Message");

            migrationBuilder.AddPrimaryKey(
                name: "PK_T_Hostel_Message",
                table: "T_Hostel_Message",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_T_Hostel_Message",
                table: "T_Hostel_Message");

            migrationBuilder.RenameTable(
                name: "T_Hostel_Message",
                newName: "Messages");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Messages",
                table: "Messages",
                column: "Id");
        }
    }
}
