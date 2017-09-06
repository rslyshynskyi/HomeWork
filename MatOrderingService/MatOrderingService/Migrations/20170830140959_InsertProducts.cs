using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MatOrderingService.Migrations
{
    public partial class InsertProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Products (Code, Name) VALUES ('sikubo', 'Table');");
            migrationBuilder.Sql("INSERT INTO Products (Code, Name) VALUES ('hokeri', 'Chair');");
            migrationBuilder.Sql("INSERT INTO Products (Code, Name) VALUES ('veralo', 'Phone');");
            migrationBuilder.Sql("INSERT INTO Products (Code, Name) VALUES ('latuwu', 'Chocolate');");
            migrationBuilder.Sql("INSERT INTO Products (Code, Name) VALUES ('vikeqo', 'Computer');");
            migrationBuilder.Sql("INSERT INTO Products (Code, Name) VALUES ('bihezu', 'Door');");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
