using FluentMigrator;

namespace SpotBot.Server.Database.Migrations
{
    [Migration(0000000400)]
    public class EncryptionKeyTableMigration : Migration
    {
        public override void Up()
        {
            Create.Table("EncryptionKey")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("UserId").AsInt32().NotNullable().ForeignKey("FK_EncryptionKey_UserId", "User", "Id")
                .WithColumn("ObfuscatedValue").AsString().NotNullable();
            Create.Index("IX_EncryptionKey_UserId")
                .OnTable("EncryptionKey")
                .OnColumn("UserId");
        }

        public override void Down()
        {
            // Remove the EncryptionKeys table
            Delete.Table("EncryptionKey");
        }
    }
}
