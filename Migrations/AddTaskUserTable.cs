using FluentMigrator;

namespace Badgage.Migrations
{
    [Migration(5)]
    public class AddTaskUserTable : Migration
    {
        public override void Down()
        {
            Delete.Table("TaskUser");
        }

        public override void Up()
        {
            Create.Table("TaskUser")
                .WithColumn("idUser").AsInt32().ForeignKey("User", "idUtil")
                .WithColumn("idTask").AsInt32().ForeignKey("Task", "idtache");
        }
    }
}
