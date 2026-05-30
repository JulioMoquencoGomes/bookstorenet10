using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStore.Api.Migrations
{
    /// <inheritdoc />
    public partial class TablesBookAndReaderStringNotEmpty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
                migrationBuilder.Sql("ALTER TABLE \"Books\" ADD CONSTRAINT \"CHK_Books_Name_NotEmpty\" CHECK (\"Name\" <> '');");
                migrationBuilder.Sql("ALTER TABLE \"Books\" ADD CONSTRAINT \"CHK_Books_Author_NotEmpty\" CHECK (\"Author\" <> '');");
                migrationBuilder.Sql("ALTER TABLE \"Books\" ADD CONSTRAINT \"CHK_Books_Urlimg_NotEmpty\" CHECK (\"Urlimg\" <> '');");

                migrationBuilder.Sql("ALTER TABLE \"Readers\" ADD CONSTRAINT \"CHK_Readers_Name_NotEmpty\" CHECK (\"Name\" <> '');");
                migrationBuilder.Sql("ALTER TABLE \"Readers\" ADD CONSTRAINT \"CHK_Readers_Urlimg_NotEmpty\" CHECK (\"Urlimg\" <> '');");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("ALTER TABLE Books DROP CONSTRAINT CHK_Books_Name_NotEmpty;");
            migrationBuilder.Sql("ALTER TABLE Readers DROP CONSTRAINT CHK_Books_Author_NotEmpty;");
            
            migrationBuilder.Sql("ALTER TABLE Books DROP CONSTRAINT CHK_Books_Urlimg_NotEmpty;");

            migrationBuilder.Sql("ALTER TABLE Books DROP CONSTRAINT CHK_Readers_Name_NotEmpty;");
            migrationBuilder.Sql("ALTER TABLE Readers DROP CONSTRAINT CHK_Readers_Urlimg_NotEmpty;");
        }
    }
}
