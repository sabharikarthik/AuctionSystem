using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AuctionSystemCore.AuctionSystemCore.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "events",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(unicode: false, maxLength: 2000, nullable: false),
                    start_time = table.Column<DateTime>(type: "datetime", nullable: false),
                    end_time = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_events", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "sold_items",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    event_id = table.Column<int>(nullable: false),
                    name = table.Column<string>(unicode: false, maxLength: 2000, nullable: false),
                    base_price = table.Column<double>(nullable: false),
                    increment_limit = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sold_items", x => x.id);
                    table.ForeignKey(
                        name: "FK__sold_item__event__286302EC",
                        column: x => x.event_id,
                        principalTable: "events",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "bids",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    user_id = table.Column<int>(nullable: false),
                    sold_item_id = table.Column<int>(nullable: false),
                    start_bid = table.Column<double>(nullable: false),
                    max_bid = table.Column<double>(nullable: false),
                    increment_amount = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bids", x => x.id);
                    table.ForeignKey(
                        name: "FK__bids__sold_item___2C3393D0",
                        column: x => x.sold_item_id,
                        principalTable: "sold_items",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__bids__user_id__2B3F6F97",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_bids_sold_item_id",
                table: "bids",
                column: "sold_item_id");

            migrationBuilder.CreateIndex(
                name: "IX_bids_user_id",
                table: "bids",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_sold_items_event_id",
                table: "sold_items",
                column: "event_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "bids");

            migrationBuilder.DropTable(
                name: "sold_items");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "events");
        }
    }
}
