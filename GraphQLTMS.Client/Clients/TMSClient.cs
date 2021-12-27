using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace GraphQLTMS.Client.Clients
{

    public interface ITMSClient
    {
        Task<T> RunQueryAsync<T>(string query);
    }

    public class TMSClient : ITMSClient
    {
        private readonly GraphQLHttpClient _graphqlClient;

        public TMSClient(IWebAssemblyHostEnvironment hostEnvironment)
        {
            _graphqlClient = new GraphQLHttpClient($"{hostEnvironment.BaseAddress}graphql", new NewtonsoftJsonSerializer());
        }

        public async Task<T> RunQueryAsync<T>(string query)
        {
            var request = new GraphQLRequest(query);
            var response = await _graphqlClient.SendQueryAsync<T>(request);

            return response.Data;
        }
    }
}
