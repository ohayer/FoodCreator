using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Food_Creator.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dishes",
                columns: table => new
                {
                    DishId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dishes", x => x.DishId);
                });

            migrationBuilder.CreateTable(
                name: "Ingredients",
                columns: table => new
                {
                    IngredientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.IngredientId);
                });

            migrationBuilder.CreateTable(
                name: "DishIngredients",
                columns: table => new
                {
                    DishId = table.Column<int>(type: "int", nullable: false),
                    IngredientId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DishIngredients", x => new { x.DishId, x.IngredientId });
                    table.ForeignKey(
                        name: "FK_DishIngredients_Dishes_DishId",
                        column: x => x.DishId,
                        principalTable: "Dishes",
                        principalColumn: "DishId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DishIngredients_Ingredients_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredients",
                        principalColumn: "IngredientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Dishes",
                columns: new[] { "DishId", "Name", "Price", "Url" },
                values: new object[,]
                {
                    { 1, "Pizza", 9.99f, "https://pizzapozachodzie.pl/wp-content/uploads/2021/10/pizzeria-template-header-pizza-img.png" },
                    { 2, "Burger", 5.99f, "https://www.maxpremiumburgers.pl/globalassets/images/pl-menu/burgery/2024/original_noketchup_singel.jpg?width=1160&sharpen=5&sigma=1,4&threshold=0" }
                });

            migrationBuilder.InsertData(
                table: "Ingredients",
                columns: new[] { "IngredientId", "Name", "Price", "Url" },
                values: new object[,]
                {
                    { 1, "Tomato", 0.5f, "https://5.imimg.com/data5/RU/NM/CJ/SELLER-27229577/tomato-500x500.jpg" },
                    { 2, "Cheese", 1.5f, "https://sklep.spolemkielce.pl/wp-content/uploads/2024/06/ser-z-oczkiem-wloszczowa.jpg" },
                    { 3, "Basil", 0.2f, "https://p.fide.pl/1600/7/8/78303_veritable_podstawowe_ziola_wklad_nasienny_bazylia.jpg" },
                    { 4, "Beef 100g", 5f, "https://smakigarwolina.pl/wp-content/uploads/2017/06/wolowina-bez-kosci.jpg" },
                    { 5, "Bacon 30g", 2.88f, "https://media.istockphoto.com/id/508755080/pl/zdj%C4%99cie/gotowane-plasterki-bekonu-zbli%C5%BCenie-odizolowane-na-na-bia%C5%82e-t%C5%82o.jpg?s=612x612&w=0&k=20&c=SSJBEO0G3F4r83gYWe6zYaKq349RB4J8QyONEyzgJ7c=" }
                });

            migrationBuilder.InsertData(
                table: "DishIngredients",
                columns: new[] { "DishId", "IngredientId", "Quantity" },
                values: new object[,]
                {
                    { 1, 1, 2f },
                    { 1, 2, 1f },
                    { 1, 3, 3f },
                    { 2, 1, 4f },
                    { 2, 4, 2f },
                    { 2, 5, 1f }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DishIngredients_IngredientId",
                table: "DishIngredients",
                column: "IngredientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DishIngredients");

            migrationBuilder.DropTable(
                name: "Dishes");

            migrationBuilder.DropTable(
                name: "Ingredients");
        }
    }
}
