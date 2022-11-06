using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serializers;
using RestSharp.Serializers.NewtonsoftJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.WebServiceClients.RestSharp
{
    public class RestSharpClientHelper: IClient
    {
        private static RestClient _restClient = new ();

        public Dictionary<string, string> RequestHeaders { get; set; }

        public Dictionary<string, string> RequestParameters { get; set; }

        public RestResponse Response { get; set; }

        private readonly string baseUrl;

        public RestSharpClientHelper(string baseUrl)
        {
            this.baseUrl = baseUrl;
            _restClient.UseNewtonsoftJson();
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

        public T GetRequest<T>(string endpoint, T? payload)
        {
            return SendRequest(Method.Get, new Uri($"{baseUrl}{endpoint}"), payload);
        }

        public T PostRequest<T>(string endpoint, T? payload)
        {
            return SendRequest(Method.Post, new Uri($"{baseUrl}{endpoint}"), payload);
        }

        public T PutRequest<T>(string endpoint, T? payload)
        {
            return SendRequest(Method.Put, new Uri($"{baseUrl}{endpoint}"), payload);
        }

        public T PatchRequest<T>(string endpoint, T? payload)
        {
            return SendRequest(Method.Patch, new Uri($"{baseUrl}{endpoint}"), payload);
        }

        public string DeleteRequest(string endpoint)
        {
            return SendRequest<string>(Method.Delete, new Uri($"{baseUrl}{endpoint}"), null);
        }

        public T SendRequest<T>(Method method, Uri uri, T? payload)
        {
            var request = new RestRequest(uri, method);
            
            if (RequestHeaders != null)
                request.AddHeaders(RequestHeaders);

            if (RequestParameters != null)
                request = AddParameters(request);

            if (payload != null) 
            {
                var requestBody = JsonConvert.SerializeObject(payload, Formatting.Indented);
                request.AddBody(requestBody);
            }
               
            var response = _restClient.Execute<T>(request);
            
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

        public HttpResponseMessage GetResponse()
        {
            throw new NotImplementedException();
        }
    }
}
