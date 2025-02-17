using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BasicWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddCountryForeignKeyToContact : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "Contacts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_CountryId",
                table: "Contacts",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_Countries_CountryId",
                table: "Contacts",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "CountryId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_Countries_CountryId",
                table: "Contacts");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_CountryId",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Contacts");
        }
    }
}
