using FluentMigrator;

namespace SpotBot.Server.Database.Migrations
{
    [Migration(0000000500)]
    public class TradeEntryTableMigration : Migration
    {
        public override void Up()
        {
            Create.Table("TradeEntry")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("PositionSize").AsDecimal().NotNullable()
                .WithColumn("EntryPrice").AsDecimal().NotNullable()
                .WithColumn("StopLoss").AsDecimal().NotNullable()
                .WithColumn("TakeProfit").AsDecimal().NotNullable()
                .WithColumn("TradeType").AsString(4).NotNullable();
        }

        public override void Down()
        {
            Delete.Table("TradeEntry");
        }
    }
}