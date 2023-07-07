namespace SpotBot.Server.Api.Responses.Shapes
{
    public class AccountShape
    {
        public AccountShape()
        {
            Id = string.Empty;
            Currency = string.Empty;
            Type = string.Empty;
            Balance = 0m;
            Available = 0m;
            Holds = 0m;
        }
        public string Id { get; set; }
        public string Currency { get; set; }
        public string Type { get; set; }
        public decimal Balance { get; set; }
        public decimal Available { get; set; }
        public decimal Holds { get; set; }
    }
}
