using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Food_Creator.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Url",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "Dishes");

            migrationBuilder.RenameColumn(
                name: "ImageData",
                table: "Ingredients",
                newName: "Image");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Ingredients",
                newName: "ImageData");

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Ingredients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Dishes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Dishes",
                keyColumn: "DishId",
                keyValue: 1,
                column: "Url",
                value: "https://pizzapozachodzie.pl/wp-content/uploads/2021/10/pizzeria-template-header-pizza-img.png");

            migrationBuilder.UpdateData(
                table: "Dishes",
                keyColumn: "DishId",
                keyValue: 2,
                column: "Url",
                value: "https://www.maxpremiumburgers.pl/globalassets/images/pl-menu/burgery/2024/original_noketchup_singel.jpg?width=1160&sharpen=5&sigma=1,4&threshold=0");

            migrationBuilder.UpdateData(
                table: "Ingredients",
                keyColumn: "IngredientId",
                keyValue: 1,
                column: "Url",
                value: "https://5.imimg.com/data5/RU/NM/CJ/SELLER-27229577/tomato-500x500.jpg");

            migrationBuilder.UpdateData(
                table: "Ingredients",
                keyColumn: "IngredientId",
                keyValue: 2,
                column: "Url",
                value: "https://sklep.spolemkielce.pl/wp-content/uploads/2024/06/ser-z-oczkiem-wloszczowa.jpg");

            migrationBuilder.UpdateData(
                table: "Ingredients",
                keyColumn: "IngredientId",
                keyValue: 3,
                column: "Url",
                value: "https://p.fide.pl/1600/7/8/78303_veritable_podstawowe_ziola_wklad_nasienny_bazylia.jpg");

            migrationBuilder.UpdateData(
                table: "Ingredients",
                keyColumn: "IngredientId",
                keyValue: 4,
                column: "Url",
                value: "https://smakigarwolina.pl/wp-content/uploads/2017/06/wolowina-bez-kosci.jpg");

            migrationBuilder.UpdateData(
                table: "Ingredients",
                keyColumn: "IngredientId",
                keyValue: 5,
                column: "Url",
                value: "https://media.istockphoto.com/id/508755080/pl/zdj%C4%99cie/gotowane-plasterki-bekonu-zbli%C5%BCenie-odizolowane-na-na-bia%C5%82e-t%C5%82o.jpg?s=612x612&w=0&k=20&c=SSJBEO0G3F4r83gYWe6zYaKq349RB4J8QyONEyzgJ7c=");
        }
    }
}
