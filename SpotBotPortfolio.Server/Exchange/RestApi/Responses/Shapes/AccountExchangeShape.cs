namespace SpotBot.Server.Exchange.RestApi.Responses.Shapes
{
    internal class AccountExchangeShape
    {
        public AccountExchangeShape()
        {
            Id = string.Empty;
            Currency = string.Empty;
            Type = string.Empty;
            Balance = 0m;
            Available = 0m;
            Holds = 0m;
        }
        public string Id { get; set; }          // The ID of the account
        public string Currency { get; set; }    // The currency of the account
        public string Type { get; set; }        // The type of the account
        public decimal Balance { get; set; }    // The total funds in the account
        public decimal Available { get; set; }  // The funds available to withdraw or trade
        public decimal Holds { get; set; }      // The funds on hold (not available for use)
    }
}
