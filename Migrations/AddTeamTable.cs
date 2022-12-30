using FluentMigrator;

namespace Badgage.Migrations
{
    [Migration(2)]
    public class AddTeamTable : ForwardOnlyMigration
    {
        public override void Up()
        {
            Create.Table("Team")
                .WithColumn("idTeam").AsInt32().PrimaryKey().Identity()
                .WithColumn("nom").AsString()
                .WithColumn("byUser").AsInt32().ForeignKey("User", "idUtil");
        }
    }
}
