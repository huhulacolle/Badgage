using FluentMigrator;

namespace Badgage.Migrations
{
    [Migration(4)]
    public class AddProjectUserTable : ForwardOnlyMigration
    {
        public override void Up()
        {
            Create.Table("ProjectUser")
                .WithColumn("idUser").AsInt32().ForeignKey("User", "idUtil")
                .WithColumn("idProject").AsInt32().ForeignKey("Project", "idProject");
        }
    }
}
