using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UrlShortener.Migrations
{
    /// <inheritdoc />
    public partial class newModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AppUrlId",
                table: "AspNetUserTokens",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AppUrlId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AppUrlId",
                table: "AspNetUserRoles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AppUrlId",
                table: "AspNetUserLogins",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AppUrlId",
                table: "AspNetUserClaims",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AppUrlId",
                table: "AspNetRoles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AppUrlId",
                table: "AspNetRoleClaims",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserTokens_AppUrlId",
                table: "AspNetUserTokens",
                column: "AppUrlId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_AppUrlId",
                table: "AspNetUsers",
                column: "AppUrlId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_AppUrlId",
                table: "AspNetUserRoles",
                column: "AppUrlId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_AppUrlId",
                table: "AspNetUserLogins",
                column: "AppUrlId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_AppUrlId",
                table: "AspNetUserClaims",
                column: "AppUrlId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoles_AppUrlId",
                table: "AspNetRoles",
                column: "AppUrlId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_AppUrlId",
                table: "AspNetRoleClaims",
                column: "AppUrlId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoleClaims_AppUrl_AppUrlId",
                table: "AspNetRoleClaims",
                column: "AppUrlId",
                principalTable: "AppUrl",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoles_AppUrl_AppUrlId",
                table: "AspNetRoles",
                column: "AppUrlId",
                principalTable: "AppUrl",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AppUrl_AppUrlId",
                table: "AspNetUserClaims",
                column: "AppUrlId",
                principalTable: "AppUrl",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AppUrl_AppUrlId",
                table: "AspNetUserLogins",
                column: "AppUrlId",
                principalTable: "AppUrl",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AppUrl_AppUrlId",
                table: "AspNetUserRoles",
                column: "AppUrlId",
                principalTable: "AppUrl",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AppUrl_AppUrlId",
                table: "AspNetUsers",
                column: "AppUrlId",
                principalTable: "AppUrl",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AppUrl_AppUrlId",
                table: "AspNetUserTokens",
                column: "AppUrlId",
                principalTable: "AppUrl",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoleClaims_AppUrl_AppUrlId",
                table: "AspNetRoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoles_AppUrl_AppUrlId",
                table: "AspNetRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_AppUrl_AppUrlId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_AppUrl_AppUrlId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AppUrl_AppUrlId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AppUrl_AppUrlId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_AppUrl_AppUrlId",
                table: "AspNetUserTokens");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserTokens_AppUrlId",
                table: "AspNetUserTokens");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_AppUrlId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserRoles_AppUrlId",
                table: "AspNetUserRoles");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserLogins_AppUrlId",
                table: "AspNetUserLogins");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserClaims_AppUrlId",
                table: "AspNetUserClaims");

            migrationBuilder.DropIndex(
                name: "IX_AspNetRoles_AppUrlId",
                table: "AspNetRoles");

            migrationBuilder.DropIndex(
                name: "IX_AspNetRoleClaims_AppUrlId",
                table: "AspNetRoleClaims");

            migrationBuilder.DropColumn(
                name: "AppUrlId",
                table: "AspNetUserTokens");

            migrationBuilder.DropColumn(
                name: "AppUrlId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "AppUrlId",
                table: "AspNetUserRoles");

            migrationBuilder.DropColumn(
                name: "AppUrlId",
                table: "AspNetUserLogins");

            migrationBuilder.DropColumn(
                name: "AppUrlId",
                table: "AspNetUserClaims");

            migrationBuilder.DropColumn(
                name: "AppUrlId",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "AppUrlId",
                table: "AspNetRoleClaims");
        }
    }
}
