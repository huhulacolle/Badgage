using FluentMigrator;

namespace Badgage.Migrations
{
    [Migration(2)]
    public class AddRoleTable : Migration
    {
        public override void Down()
        {
            Delete.Table("Role");
        }

        public override void Up()
        {
            Create.Table("Role")
                .WithColumn("idRole").AsInt32().PrimaryKey()
                .WithColumn("libelle").AsString().NotNullable().Unique();
        }
    }
}
