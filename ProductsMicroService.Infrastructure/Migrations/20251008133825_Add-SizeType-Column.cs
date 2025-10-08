using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductsMicroService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSizeTypeColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SizeType",
                table: "Sizes",
                type: "longtext",
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SizeType",
                table: "Sizes");
        }
    }
}
