using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HostelModel.Migrations
{
    public partial class accoutlevel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_T_PersonEmploy_T_Hostel_WorkOrder_HotelOrderId",
                table: "T_PersonEmploy");

            migrationBuilder.DropForeignKey(
                name: "FK_T_PersonEmploy_T_Hostel_ServicePerson_PersonId",
                table: "T_PersonEmploy");

            migrationBuilder.DropPrimaryKey(
                name: "PK_T_PersonEmploy",
                table: "T_PersonEmploy");

            migrationBuilder.RenameTable(
                name: "T_PersonEmploy",
                newName: "T_Hostel_PersonEmploy");

            migrationBuilder.RenameIndex(
                name: "IX_T_PersonEmploy_PersonId",
                table: "T_Hostel_PersonEmploy",
                newName: "IX_T_Hostel_PersonEmploy_PersonId");

            migrationBuilder.RenameIndex(
                name: "IX_T_PersonEmploy_HotelOrderId",
                table: "T_Hostel_PersonEmploy",
                newName: "IX_T_Hostel_PersonEmploy_HotelOrderId");

            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "T_Hostel_Accout",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_T_Hostel_PersonEmploy",
                table: "T_Hostel_PersonEmploy",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_T_Hostel_PersonEmploy_T_Hostel_WorkOrder_HotelOrderId",
                table: "T_Hostel_PersonEmploy",
                column: "HotelOrderId",
                principalTable: "T_Hostel_WorkOrder",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_T_Hostel_PersonEmploy_T_Hostel_ServicePerson_PersonId",
                table: "T_Hostel_PersonEmploy",
                column: "PersonId",
                principalTable: "T_Hostel_ServicePerson",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_T_Hostel_PersonEmploy_T_Hostel_WorkOrder_HotelOrderId",
                table: "T_Hostel_PersonEmploy");

            migrationBuilder.DropForeignKey(
                name: "FK_T_Hostel_PersonEmploy_T_Hostel_ServicePerson_PersonId",
                table: "T_Hostel_PersonEmploy");

            migrationBuilder.DropPrimaryKey(
                name: "PK_T_Hostel_PersonEmploy",
                table: "T_Hostel_PersonEmploy");

            migrationBuilder.DropColumn(
                name: "Level",
                table: "T_Hostel_Accout");

            migrationBuilder.RenameTable(
                name: "T_Hostel_PersonEmploy",
                newName: "T_PersonEmploy");

            migrationBuilder.RenameIndex(
                name: "IX_T_Hostel_PersonEmploy_PersonId",
                table: "T_PersonEmploy",
                newName: "IX_T_PersonEmploy_PersonId");

            migrationBuilder.RenameIndex(
                name: "IX_T_Hostel_PersonEmploy_HotelOrderId",
                table: "T_PersonEmploy",
                newName: "IX_T_PersonEmploy_HotelOrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_T_PersonEmploy",
                table: "T_PersonEmploy",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_T_PersonEmploy_T_Hostel_WorkOrder_HotelOrderId",
                table: "T_PersonEmploy",
                column: "HotelOrderId",
                principalTable: "T_Hostel_WorkOrder",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_T_PersonEmploy_T_Hostel_ServicePerson_PersonId",
                table: "T_PersonEmploy",
                column: "PersonId",
                principalTable: "T_Hostel_ServicePerson",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
