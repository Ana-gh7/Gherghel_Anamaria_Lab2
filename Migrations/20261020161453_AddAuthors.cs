
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gherghel_Anamaria_Lab2.Migrations
{
    public partial class AddAuthors : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1. Cream tabelul Authors.
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.ID);
                });

            // 2. Adaugam temporar AuthorsID, care poate fi NULL.
            migrationBuilder.AddColumn<int>(
                name: "AuthorsID",
                table: "Book",
                type: "int",
                nullable: true);


            // 3. Copiem autorii existenti in tabelul Authors.
            migrationBuilder.Sql(@"
                INSERT INTO Authors (FirstName, LastName)
                SELECT 'Liviu', 'Rebreanu'
                WHERE EXISTS (
                    SELECT 1 FROM Book WHERE Author = 'Liviu Rebreanu'
                );

                INSERT INTO Authors (FirstName, LastName)
                SELECT 'Marin', 'Preda'
                WHERE EXISTS (
                    SELECT 1 FROM Book WHERE Author = 'Marin Preda'
                );
            ");

            // 4. Asociem cartile cu autorii lor.
            migrationBuilder.Sql(@"
                UPDATE Book
                SET AuthorsID = (
                    SELECT TOP 1 ID
                    FROM Authors
                    WHERE FirstName = 'Liviu'
                      AND LastName = 'Rebreanu'
                )
                WHERE Author = 'Liviu Rebreanu';

                UPDATE Book
                SET AuthorsID = (
                    SELECT TOP 1 ID
                    FROM Authors
                    WHERE FirstName = 'Marin'
                      AND LastName = 'Preda'
                )
                WHERE Author = 'Marin Preda';
            ");

            // 5. Tratam eventualele carti cu alti autori.
            migrationBuilder.Sql(@"
                IF EXISTS (SELECT 1 FROM Book WHERE AuthorsID IS NULL)
                BEGIN
                    INSERT INTO Authors (FirstName, LastName)
                    VALUES ('Unknown', 'Author');

                    UPDATE Book
                    SET AuthorsID = (
                        SELECT TOP 1 ID
                        FROM Authors
                        WHERE FirstName = 'Unknown'
                          AND LastName = 'Author'
                    )
                    WHERE AuthorsID IS NULL;
                END;
            ");

            // 6. Facem AuthorsID obligatoriu.
            migrationBuilder.AlterColumn<int>(
                name: "AuthorsID",
                table: "Book",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            // 7. Cream relatia dintre Book si Authors.
            migrationBuilder.CreateIndex(
                name: "IX_Book_AuthorsID",
                table: "Book",
                column: "AuthorsID");

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Authors_AuthorsID",
                table: "Book",
                column: "AuthorsID",
                principalTable: "Authors",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            // 8. Stergem vechea coloana abia dupa copierea datelor.
            migrationBuilder.DropColumn(
                name: "Author",
                table: "Book");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Eliminam relatia cu Authors.
            migrationBuilder.DropForeignKey(
                name: "FK_Book_Authors_AuthorsID",
                table: "Book");

            migrationBuilder.DropIndex(
                name: "IX_Book_AuthorsID",
                table: "Book");

            // Restauram coloana Author.
            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "Book",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            // Copiem numele autorilor inapoi in coloana Author.
            migrationBuilder.Sql(@"
                UPDATE B
                SET B.Author = LTRIM(RTRIM(
                    A.FirstName + ' ' + A.LastName
                ))
                FROM Book AS B
                INNER JOIN Authors AS A
                    ON B.AuthorsID = A.ID;
            ");

            migrationBuilder.DropColumn(
                name: "AuthorsID",
                table: "Book");

            migrationBuilder.DropTable(
                name: "Authors");
        }
    }
}
