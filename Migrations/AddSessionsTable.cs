using FluentMigrator;

namespace Badgage.Migrations
{
    [Migration(9)]
    public class AddSessionsTable : ForwardOnlyMigration
    {
        public override void Up()
        {
            Create.Table("Sessions")
                .WithColumn("id").AsInt32().PrimaryKey().Identity()
                .WithColumn("idTask").AsInt32().ForeignKey("Task", "idTask")
                .WithColumn("DateDebut").AsDateTime()
                .WithColumn("DateFin").AsDateTime();
        }
    }
}
