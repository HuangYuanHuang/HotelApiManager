using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HostelModel.Migrations
{
    public partial class UpdateServicePerson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "T_Hostel_ServicePerson",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Address = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    GUID = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false),
                    Icon = table.Column<string>(type: "longtext", nullable: true),
                    IdentityCard = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false),
                    Phone = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Pwd = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false),
                    RealName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Sex = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Hostel_ServicePerson", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "T_Hostel_ServicePerson");
        }
    }
}
