using Microsoft.EntityFrameworkCore.Migrations;

namespace JuliaFlorea.DataModel.Migrations
{
    public partial class AddedWeightToDrugType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "WeightInPounds",
                table: "DrugTypes",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WeightInPounds",
                table: "DrugTypes");
        }
    }
}
