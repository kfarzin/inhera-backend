using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inhera.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Additional_Service_Types : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CancelAt",
                table: "Subscriptions");

            migrationBuilder.AddColumn<string>(
                name: "AccessKey",
                table: "LabCenters",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasAccess",
                table: "LabCenters",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "AdditionalServices",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "LabCenterCalendars",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    LabCenterId = table.Column<Guid>(type: "uuid", nullable: false),
                    DateIdentifier = table.Column<DateOnly>(type: "date", nullable: false),
                    TimeSlots = table.Column<string>(type: "jsonb", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabCenterCalendars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LabCenterCalendars_LabCenters_LabCenterId",
                        column: x => x.LabCenterId,
                        principalTable: "LabCenters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LabCenterCalendars_LabCenterId_DateIdentifier",
                table: "LabCenterCalendars",
                columns: new[] { "LabCenterId", "DateIdentifier" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LabCenterCalendars");

            migrationBuilder.DropColumn(
                name: "AccessKey",
                table: "LabCenters");

            migrationBuilder.DropColumn(
                name: "HasAccess",
                table: "LabCenters");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "AdditionalServices");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CancelAt",
                table: "Subscriptions",
                type: "timestamp with time zone",
                nullable: true);
        }
    }
}
