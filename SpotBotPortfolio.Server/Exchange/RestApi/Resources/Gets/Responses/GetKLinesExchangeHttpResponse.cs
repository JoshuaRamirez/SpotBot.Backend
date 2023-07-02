namespace SpotBot.Server.Exchange.RestApi.Resources.Gets.Responses
{
    internal class GetKLinesExchangeHttpResponse
    {
        public GetKLinesExchangeHttpResponse()
        {
            Code = "";
            Data = new List<List<string>>();
        }
        public string Code;
        public List<List<string>> Data;
    }
}
