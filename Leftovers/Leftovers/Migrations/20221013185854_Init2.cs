using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Leftovers.Migrations
{
    public partial class Init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ChainId",
                table: "Restaurants",
                newName: "MealId");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Meals",
                newName: "Price");

            migrationBuilder.AddColumn<int>(
                name: "RestaurantId",
                table: "Chains",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Restaurants_MealId",
                table: "Restaurants",
                column: "MealId");

            migrationBuilder.CreateIndex(
                name: "IX_Chains_RestaurantId",
                table: "Chains",
                column: "RestaurantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chains_Restaurants_RestaurantId",
                table: "Chains",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurants_Meals_MealId",
                table: "Restaurants",
                column: "MealId",
                principalTable: "Meals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chains_Restaurants_RestaurantId",
                table: "Chains");

            migrationBuilder.DropForeignKey(
                name: "FK_Restaurants_Meals_MealId",
                table: "Restaurants");

            migrationBuilder.DropIndex(
                name: "IX_Restaurants_MealId",
                table: "Restaurants");

            migrationBuilder.DropIndex(
                name: "IX_Chains_RestaurantId",
                table: "Chains");

            migrationBuilder.DropColumn(
                name: "RestaurantId",
                table: "Chains");

            migrationBuilder.RenameColumn(
                name: "MealId",
                table: "Restaurants",
                newName: "ChainId");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Meals",
                newName: "Description");
        }
    }
}
