using FluentMigrator;

namespace SpotBot.Server.Database.Migrations
{
    [Migration(0000000300)]
    public class UserTokenTableMigration : Migration
    {
        public override void Up()
        {
            Create.Table("UserToken")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("UserId").AsInt32().NotNullable().ForeignKey("FK_Token_UserId", "User", "Id")
                .WithColumn("Token").AsGuid().NotNullable()
                .WithColumn("CreationTime").AsDateTime().NotNullable()
                .WithColumn("ExpirationTime").AsDateTime().NotNullable()
                .WithColumn("LastActivityTime").AsDateTime().NotNullable()
                .WithColumn("UserAgent").AsString().NotNullable()
                .WithColumn("IpAddress").AsString().NotNullable();

            Create.Index("IX_UserToken_UserId")
                .OnTable("UserToken")
                .OnColumn("UserId");

            Create.Index("IX_UserToken_ExpirationTime")
                .OnTable("UserToken")
                .OnColumn("ExpirationTime");
        }

        public override void Down()
        {
            Delete.Table("UserToken");
        }
    }
}
