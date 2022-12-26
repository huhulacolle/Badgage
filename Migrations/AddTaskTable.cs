using FluentMigrator;

namespace Badgage.Migrations
{
    [Migration(2)]
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
                .WithColumn("nomdetache").AsString().NotNullable().Unique()
                .WithColumn("description").AsString().NotNullable().Unique()
                .WithColumn("datefin").AsDateTime().NotNullable().Unique()
                .WithColumn("datecreation").AsDateTime().NotNullable().Unique();
        }
    }
}
