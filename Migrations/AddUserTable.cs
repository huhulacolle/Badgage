using FluentMigrator;

namespace Badgage.Migrations
{
    [Migration(1)]
    public class AddUserTable : Migration
    {
        public override void Down()
        {
            Delete.Table("User");
        }

        public override void Up()
        {
            Create.Table("User")
                .WithColumn("idUtil").AsInt32().PrimaryKey().Identity()
                .WithColumn("prenom").AsString()
                .WithColumn("nom").AsString()
                .WithColumn("datenaiss").AsDate().Nullable()
                .WithColumn("adressemail").AsString().Unique()
                .WithColumn("mdp").AsString();
        }
    }
}
