using FluentMigrator;

namespace Badgage.Migrations
{
    [Migration(8)]
    public class AddTeamUserTable : ForwardOnlyMigration
    {
        public override void Up()
        {
            Create.Table("TeamUser")
                .WithColumn("idUser").AsInt32().ForeignKey("User", "idUtil")
                .WithColumn("idTeam").AsInt32().ForeignKey("Team", "idTeam");
        }
    }
}
