﻿using FluentMigrator;

namespace Badgage.Migrations
{
    [Migration(5)]
    public class AddTaskTable : ForwardOnlyMigration
    {
        public override void Up()
        {
            Create.Table("Task")
                .WithColumn("idTask").AsInt32().PrimaryKey().Identity()
                .WithColumn("idprojet").AsInt32().ForeignKey("Project", "idProject")
                .WithColumn("nomdetache").AsString()
                .WithColumn("description").AsString().Nullable()
                .WithColumn("datefin").AsDateTime().Nullable()
                .WithColumn("datecreation").AsDateTime();
        }
    }
}
