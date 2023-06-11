using FluentMigrator;

namespace SpotBot.Server.Database.Migrations
{
    [Migration(0000000200)]
    public class AddLogTableMigration : Migration
    {
        public override void Up()
        {
            Create.Table("Exchange")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("UserId").AsInt32().NotNullable().ForeignKey("FK_Exchange_UserId", "User", "Id")
                .WithColumn("ApiPublicKey").AsString(100).NotNullable().Unique()
                .WithColumn("ApiPrivateKey").AsString(100).NotNullable().Unique()
                .WithColumn("ApiKeyPassphrase").AsString(100).NotNullable()
                .WithColumn("ApiVersion").AsString(10).NotNullable();
            Create.Index("IX_Exchange_UserId")
                .OnTable("Exchange")
                .OnColumn("UserId");
        }

        public override void Down()
        {
            Delete.Table("ExchangeCredential");
        }
    }
}
