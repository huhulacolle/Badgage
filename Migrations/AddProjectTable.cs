using FluentMigrator;

namespace Badgage.Migrations
{
    [Migration(2)]
    public class AddProjectTable : Migration
    {
        public override void Down()
        {
            Delete.Table("Project");
        }

        public override void Up()
        {
            Create.Table("Project")
                .WithColumn("idProject").AsInt32().PrimaryKey().Identity()
                .WithColumn("projectName").AsString().NotNullable()
                .WithColumn("ByUser").AsInt32().ForeignKey("User", "idUtil");
        }
    }
}
