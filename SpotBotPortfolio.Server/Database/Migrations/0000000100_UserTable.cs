using FluentMigrator;
using SpotBot.Server.Core;

namespace SpotBot.Server.Database.Migrations
{
    [Migration(0000000100)]
    public class UserTableMigration : Migration
    {
        public override void Up()
        {
            Create.Table("User")
                    .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                    .WithColumn("Username").AsString(100).NotNullable().Unique()
                    .WithColumn("PasswordHash").AsString(100).NotNullable()
                    .WithColumn("Email").AsString(100).NotNullable().Unique();
            Insert.IntoTable("User").Row(new
            {
                Username = "TestUser",
                PasswordHash = Hasher.Hash("TestPassword"),
                Email = "test@test-user.com"
            });
        }

        public override void Down()
        {
            Delete.Table("User");
        }
    }
}
