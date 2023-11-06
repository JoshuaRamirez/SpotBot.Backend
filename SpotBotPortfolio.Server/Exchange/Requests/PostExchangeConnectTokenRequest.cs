using SpotBot.Server.Database.Core;
using SpotBot.Server.Exchange.Api.Rest.Core;
using SpotBot.Server.Exchange.Api.Rest.Responses;
using SpotBot.Server.Exchange.Responses;
using System.Text.Json;

namespace SpotBot.Server.Exchange.Requests
{
    internal class PostExchangeConnectTokenRequest
    {
        private readonly Connection _connection;

        public PostExchangeConnectTokenRequest(Connection connection)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
        }

        //TODO: Remove the user id from thi
        public PostExchangeConnectTokenResponse Execute(int userId)
        {
            var endpoint = "api/v1/bullet-public";

            var restApiClientFactory = new RestApiClientFactory(_connection);

            //TODO: Create a factory Create overload for creating a client for public operations that don't need a user id.
            //Note: This request does not need a user id for a successful response, it's just to satisfy the factory.
            using var restApiClient = restApiClientFactory.Create(userId);
            var asyncExchangeResponse = restApiClient.PostAsync(endpoint);
            asyncExchangeResponse.Wait();
            var responseBody = asyncExchangeResponse.Result;
            if (responseBody == null)
            {
                throw new Exception("The response body is null.");
            }
            var jsonSerializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var deserializedResponse = JsonSerializer.Deserialize<PostExchangeConnectTokenRestResponse>(responseBody, jsonSerializerOptions);
            if (deserializedResponse == null)
            {
                throw new InvalidOperationException();
            }
            var postExchangeConnectTokenResponse = new PostExchangeConnectTokenResponse();
            postExchangeConnectTokenResponse.Token = deserializedResponse.Data.Token;
            return postExchangeConnectTokenResponse;
        }
    }
}
