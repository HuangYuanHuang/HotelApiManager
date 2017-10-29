using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HostelModel.Migrations
{
    public partial class useroline : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonEmploys_T_Hostel_WorkOrder_HotelOrderId",
                table: "PersonEmploys");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonEmploys_T_Hostel_ServicePerson_PersonId",
                table: "PersonEmploys");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PersonEmploys",
                table: "PersonEmploys");

            migrationBuilder.RenameTable(
                name: "PersonEmploys",
                newName: "T_PersonEmploy");

            migrationBuilder.RenameIndex(
                name: "IX_PersonEmploys_PersonId",
                table: "T_PersonEmploy",
                newName: "IX_T_PersonEmploy_PersonId");

            migrationBuilder.RenameIndex(
                name: "IX_PersonEmploys_HotelOrderId",
                table: "T_PersonEmploy",
                newName: "IX_T_PersonEmploy_HotelOrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_T_PersonEmploy",
                table: "T_PersonEmploy",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "T_Hostel_UserOnline",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AccoutType = table.Column<string>(type: "longtext", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Device = table.Column<string>(type: "longtext", nullable: true),
                    GUID = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false),
                    IMEI = table.Column<string>(type: "longtext", nullable: true),
                    LastLogin = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Phone = table.Column<string>(type: "longtext", nullable: true),
                    SoftVersion = table.Column<string>(type: "longtext", nullable: true),
                    SystemType = table.Column<string>(type: "longtext", nullable: true),
                    Token = table.Column<string>(type: "longtext", nullable: true),
                    UserGUID = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Hostel_UserOnline", x => x.Id);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_T_PersonEmploy_T_Hostel_WorkOrder_HotelOrderId",
                table: "T_PersonEmploy");

            migrationBuilder.DropForeignKey(
                name: "FK_T_PersonEmploy_T_Hostel_ServicePerson_PersonId",
                table: "T_PersonEmploy");

            migrationBuilder.DropTable(
                name: "T_Hostel_UserOnline");

            migrationBuilder.DropPrimaryKey(
                name: "PK_T_PersonEmploy",
                table: "T_PersonEmploy");

            migrationBuilder.RenameTable(
                name: "T_PersonEmploy",
                newName: "PersonEmploys");

            migrationBuilder.RenameIndex(
                name: "IX_T_PersonEmploy_PersonId",
                table: "PersonEmploys",
                newName: "IX_PersonEmploys_PersonId");

            migrationBuilder.RenameIndex(
                name: "IX_T_PersonEmploy_HotelOrderId",
                table: "PersonEmploys",
                newName: "IX_PersonEmploys_HotelOrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PersonEmploys",
                table: "PersonEmploys",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonEmploys_T_Hostel_WorkOrder_HotelOrderId",
                table: "PersonEmploys",
                column: "HotelOrderId",
                principalTable: "T_Hostel_WorkOrder",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonEmploys_T_Hostel_ServicePerson_PersonId",
                table: "PersonEmploys",
                column: "PersonId",
                principalTable: "T_Hostel_ServicePerson",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
