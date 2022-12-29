using FluentMigrator;

namespace Badgage.Migrations
{
    [Migration(4)]
    public class AddTaskTable : Migration
    {
        public override void Down()
        {
            Delete.Table("Task");
        }

        public override void Up()
        {
            Create.Table("Task")
                .WithColumn("idTask").AsInt32().PrimaryKey().Identity()
                .WithColumn("idprojet").AsInt32().ForeignKey("Project", "idProject")
                .WithColumn("idutil").AsInt32().ForeignKey("User", "idUtil")
                .WithColumn("nomdetache").AsString()
                .WithColumn("description").AsString()
                .WithColumn("datefin").AsDateTime()
                .WithColumn("datecreation").AsDateTime()
                .WithColumn("idProject").AsInt32().ForeignKey("Project", "idProject");
        }
    }
}
