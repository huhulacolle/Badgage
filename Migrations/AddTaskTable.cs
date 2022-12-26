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
                .WithColumn("idtache").AsInt32().PrimaryKey().Identity()
                .WithColumn("nomdetache").AsString()
                .WithColumn("description").AsString()
                .WithColumn("datefin").AsDateTime()
                .WithColumn("datecreation").AsDateTime();
        }
    }
}
