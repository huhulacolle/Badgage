using FluentMigrator;

namespace Badgage.Migrations
{
    [Migration(1)]
    public class AddUserTable : ForwardOnlyMigration
    {
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
