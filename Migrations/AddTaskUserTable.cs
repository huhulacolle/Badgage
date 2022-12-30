using FluentMigrator;

namespace Badgage.Migrations
{
    [Migration(7)]
    public class AddTaskUserTable : ForwardOnlyMigration
    {
        public override void Up()
        {
            Create.Table("TaskUser")
                .WithColumn("idUser").AsInt32().ForeignKey("User", "idUtil")
                .WithColumn("idTask").AsInt32().ForeignKey("Task", "idTask");
        }
    }
}
