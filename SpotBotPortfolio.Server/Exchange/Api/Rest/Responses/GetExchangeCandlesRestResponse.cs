internal class GetExchangeCandlesRestResponse
{
    public GetExchangeCandlesRestResponse()
    {
        Code = "";
        Data = new List<List<string>>();
    }
    public string Code;
    public List<List<string>> Data;
}