using RestSharp;

namespace Common.Consume
{
    public class RestClientInfraestructure
    {
        private readonly string baseUrl;
        private readonly IRestClient client;

        public RestClientInfraestructure(string baseUrl)
        {
            this.baseUrl = baseUrl;
            client = new RestClient(baseUrl);
        }

        public async Task<string> GetAsync(string endpoint)
        {
            var request = new RestRequest(endpoint, Method.Get);
            var response = await client.ExecuteAsync(request);
            return response.Content;
        }

        public async Task<string> PostAsync(string endpoint, string data)
        {
            var request = new RestRequest(endpoint, Method.Post);
            request.AddJsonBody(data);
            var response = await client.ExecuteAsync(request);
            return response.Content;
        }

        public async Task<string> PutAsync(string endpoint, string data)
        {
            var request = new RestRequest(endpoint, Method.Put);
            request.AddJsonBody(data);
            var response = await client.ExecuteAsync(request);
            return response.Content;
        }

        public async Task<string> DeleteAsync(string endpoint)
        {
            var request = new RestRequest(endpoint, Method.Delete);
            var response = await client.ExecuteAsync(request);
            return response.Content;
        }
    }
}
