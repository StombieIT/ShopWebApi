using Microsoft.EntityFrameworkCore.Migrations;

namespace ShopWebApi.Migrations
{
    public partial class CommentRatingRemoved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rating",
                table: "ProductComments");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Rating",
                table: "ProductComments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
