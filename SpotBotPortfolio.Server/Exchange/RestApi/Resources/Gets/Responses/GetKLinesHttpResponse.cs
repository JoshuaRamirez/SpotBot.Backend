namespace SpotBot.Server.Exchange.RestApi.Services
{
    public partial class GetKLinesRequest
    {
        private class GetKLinesExchangeResponse
        {
            public string Code;
            public List<List<string>> Data;
        }
    }
}
