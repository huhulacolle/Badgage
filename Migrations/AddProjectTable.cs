using FluentMigrator;

namespace Badgage.Migrations
{
    [Migration(3)]
    public class AddProjectTable : ForwardOnlyMigration
    {
        public override void Up()
        {
            Create.Table("Project")
                .WithColumn("idProject").AsInt32().PrimaryKey().Identity()
                .WithColumn("projectName").AsString().NotNullable()
                .WithColumn("idTeam").AsInt32().ForeignKey("Team", "idTeam")
                .WithColumn("ByUser").AsInt32().ForeignKey("User", "idUtil");
        }
    }
}
