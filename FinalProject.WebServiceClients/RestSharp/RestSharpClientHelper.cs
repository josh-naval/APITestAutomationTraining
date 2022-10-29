using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.WebServiceClients.RestSharp
{
    public class RestSharpClientHelper
    {
        public Dictionary<string, string> RequestHeaders { get; set; }

        public Dictionary<string, string> RequestParameters { get; set; }

        public RestResponse Response { get; set; }

        private static RestClient restClient = new RestClient();

        private readonly string baseUrl;

        public RestSharpClientHelper(string baseUrl)
        {
            this.baseUrl = baseUrl;
        }

        public void AddRequestHeaders(string key, string value)
        {
            RequestHeaders ??= new Dictionary<string, string>();

            RequestHeaders.Add(key, value);
        }

        public void AddRequestParameters(string key, string value)
        {
            RequestParameters ??= new Dictionary<string, string>();

            RequestParameters.Add(key, value);
        }

        public async Task<T> GetRequest<T>(string endpoint, T? payload)
        {
            return await SendRequest(Method.Get, new Uri($"{baseUrl}{endpoint}"), payload);
        }

        public async Task<T> PostRequest<T>(string endpoint, T? payload)
        {
            return await SendRequest(Method.Post, new Uri($"{baseUrl}{endpoint}"), payload);
        }

        public async Task<T> PutRequest<T>(string endpoint, T? payload)
        {
            return await SendRequest(Method.Put, new Uri($"{baseUrl}{endpoint}"), payload);
        }

        public async Task<T> PatchRequest<T>(string endpoint, T? payload)
        {
            return await SendRequest(Method.Patch, new Uri($"{baseUrl}{endpoint}"), payload);
        }

        public async Task<RestResponse> DeleteRequest(string endpoint)
        {
            return await SendRequest<RestResponse>(Method.Delete, new Uri($"{baseUrl}{endpoint}"), null);
        }

        private async Task<T> SendRequest<T>(Method method, Uri uri, T? payload)
        {
            var request = new RestRequest(uri, method);

            if (RequestHeaders != null)
                request.AddHeaders(RequestHeaders);

            if (RequestParameters != null)
                request = AddParameters(request);

            if (payload != null)
                request.AddBody(payload);

            var response = await restClient.ExecuteAsync<T>(request);

            Response = response;

            return response.Data!;
        }

        private RestRequest AddParameters(RestRequest restRequest)
        {
            foreach (var requestParameters in RequestParameters)
            {
                restRequest.AddQueryParameter(requestParameters.Key, requestParameters.Value);
            }

            return restRequest;
        }
    }
}
