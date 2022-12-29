using FluentMigrator;

namespace Badgage.Migrations
{
    [Migration(3)]
    public class AddProjectUserTable : Migration
    {
        public override void Down()
        {
            Delete.Table("ProjectUser");
        }

        public override void Up()
        {
            Create.Table("ProjectUser")
                .WithColumn("idUser").AsInt32().ForeignKey("User", "idUtil")
                .WithColumn("idProject").AsInt32().ForeignKey("Project", "idProject");
        }
    }
}
