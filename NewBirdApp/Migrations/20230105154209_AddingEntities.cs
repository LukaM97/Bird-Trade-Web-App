using Microsoft.EntityFrameworkCore.Migrations;

namespace NewBirdApp.Migrations
{
    public partial class AddingEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Genders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Birds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    GenderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Birds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Birds_Genders_GenderId",
                        column: x => x.GenderId,
                        principalTable: "Genders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameOfCustomer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SurnameOfCustomer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdressOfCustomer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerCity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerIdNumber = table.Column<long>(type: "bigint", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    BirdId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Birds_BirdId",
                        column: x => x.BirdId,
                        principalTable: "Birds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Genders",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Female" });

            migrationBuilder.InsertData(
                table: "Genders",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "Male" });

            migrationBuilder.InsertData(
                table: "Birds",
                columns: new[] { "Id", "GenderId", "Name", "Quantity" },
                values: new object[,]
                {
                    { 2, 1, "Patuljasta patka", 16 },
                    { 4, 1, "Papagaj rase Aleksandar", 12 },
                    { 6, 1, "Zlatni fazan", 2 },
                    { 1, 2, "Patuljasta patka", 20 },
                    { 3, 2, "Papagaj rase Aleksandar", 10 },
                    { 5, 2, "Zlatni fazan", 6 }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "AdressOfCustomer", "Amount", "BirdId", "CustomerCity", "CustomerIdNumber", "NameOfCustomer", "SurnameOfCustomer" },
                values: new object[] { 2, "Bajnatska 20", 2, 2, "Subotica", 1178395612875L, "Aleksandar", "Boric" });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "AdressOfCustomer", "Amount", "BirdId", "CustomerCity", "CustomerIdNumber", "NameOfCustomer", "SurnameOfCustomer" },
                values: new object[] { 1, "Bajnatska 20", 2, 1, "Subotica", 1178395612875L, "Aleksandar", "Boric" });

            migrationBuilder.CreateIndex(
                name: "IX_Birds_GenderId",
                table: "Birds",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_BirdId",
                table: "Orders",
                column: "BirdId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Birds");

            migrationBuilder.DropTable(
                name: "Genders");
        }
    }
}
