using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HostelModel.Migrations
{
    public partial class initCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "T_Hostel_Area",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Address = table.Column<string>(type: "varchar(1024)", maxLength: 1024, nullable: false),
                    City = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    GUID = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false),
                    Mark = table.Column<string>(type: "longtext", nullable: true),
                    Name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Hostel_Area", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_Hostel_Schedule",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    End = table.Column<string>(type: "longtext", nullable: true),
                    GUID = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false),
                    Mark = table.Column<string>(type: "varchar(1024)", maxLength: 1024, nullable: true),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Start = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Hostel_Schedule", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_Hostel_WorkType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Duty = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false),
                    GUID = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false),
                    Mark = table.Column<string>(type: "varchar(1024)", maxLength: 1024, nullable: true),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Qualification = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Hostel_WorkType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_Hostel_Hotel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Address = table.Column<string>(type: "longtext", nullable: false),
                    AreaId = table.Column<int>(type: "int", nullable: false),
                    Bank = table.Column<string>(type: "longtext", nullable: false),
                    BankAccount = table.Column<string>(type: "longtext", nullable: false),
                    BankAddress = table.Column<string>(type: "longtext", nullable: false),
                    CODE = table.Column<string>(type: "longtext", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    GUID = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false),
                    MailingAddress = table.Column<string>(type: "longtext", nullable: false),
                    Mark = table.Column<string>(type: "varchar(1024)", maxLength: 1024, nullable: true),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Phone = table.Column<string>(type: "longtext", nullable: false),
                    Type = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Hostel_Hotel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_T_Hostel_Hotel_T_Hostel_Area_AreaId",
                        column: x => x.AreaId,
                        principalTable: "T_Hostel_Area",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "T_Hostel_Accout",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AccoutType = table.Column<int>(type: "int", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    GUID = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false),
                    HotelId = table.Column<int>(type: "int", nullable: false),
                    LastTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    LoginName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Mark = table.Column<string>(type: "varchar(1024)", maxLength: 1024, nullable: true),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Phone = table.Column<string>(type: "longtext", nullable: false),
                    Pwd = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Hostel_Accout", x => x.Id);
                    table.ForeignKey(
                        name: "FK_T_Hostel_Accout_T_Hostel_Hotel_HotelId",
                        column: x => x.HotelId,
                        principalTable: "T_Hostel_Hotel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_T_Hostel_Accout_HotelId",
                table: "T_Hostel_Accout",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_T_Hostel_Hotel_AreaId",
                table: "T_Hostel_Hotel",
                column: "AreaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "T_Hostel_Accout");

            migrationBuilder.DropTable(
                name: "T_Hostel_Schedule");

            migrationBuilder.DropTable(
                name: "T_Hostel_WorkType");

            migrationBuilder.DropTable(
                name: "T_Hostel_Hotel");

            migrationBuilder.DropTable(
                name: "T_Hostel_Area");
        }
    }
}
