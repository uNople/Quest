using Microsoft.EntityFrameworkCore.Migrations;

namespace QuestUi.Migrations
{
    public partial class Changetypeforpriority : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn("Priority", "Quests", "PriorityOld");

            migrationBuilder.AddColumn<int>(
                    name: "Priority",
                    table: "Quests",
                    type: "INTEGER",
                    nullable: false,
                    defaultValue: 0);

            migrationBuilder.Sql("UPDATE Quests SET Priority = 0 WHERE PriorityOld = 'Low'");
            migrationBuilder.Sql("UPDATE Quests SET Priority = 1 WHERE PriorityOld = 'Medium'");
            migrationBuilder.Sql("UPDATE Quests SET Priority = 2 WHERE PriorityOld = 'High'");

            migrationBuilder.DropColumn("PriorityOld", "Quests");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn("Priority", "Quests", "PriorityOld");

            migrationBuilder.AddColumn<string>(
                name: "Priority",
                table: "Quests",
                type: "TEXT",
                nullable: true);
            
            migrationBuilder.Sql("UPDATE Quests SET Priority = 'Low' WHERE PriorityOld = 0");
            migrationBuilder.Sql("UPDATE Quests SET Priority = 'Medium' WHERE PriorityOld = 1");
            migrationBuilder.Sql("UPDATE Quests SET Priority = 'High' WHERE PriorityOld = 2");
            
            migrationBuilder.DropColumn("PriorityOld", "Quests");
        }
    }
}
