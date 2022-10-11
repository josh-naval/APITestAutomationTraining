using Newtonsoft.Json;
using System.Text;

namespace Session2_1.Utilities
{
    public class HttpClientHelper
    {
        public Dictionary<string, string> RequestHeaders { get; set; }

        public Dictionary<string, string> RequestOptions { get; set; }

        public HttpResponseMessage Response { get; set; }

        private static readonly HttpClient Client = new();

        private readonly String _baseUrl;

        public HttpClientHelper(String baseUrl)
        {
            _baseUrl = baseUrl;
        }

        public void AddRequestHeaders(string key, string value)
        {
            RequestHeaders ??= new Dictionary<string, string>();

            RequestHeaders.Add(key, value);
        }

        public void AddRequestOptions(string key, string value)
        {
            RequestOptions ??= new Dictionary<string, string>();

            RequestOptions.Add(key, value);
        }

        private HttpRequestMessage SetRequestHeaders(HttpRequestMessage requestMessage)
        {
            foreach (var requestHeader in RequestHeaders!)
            {
                requestMessage.Headers.Add(requestHeader.Key, requestHeader.Value);
            }

            return requestMessage;
        }

        private T SendRequest<T>(HttpMethod httpMethod, Uri uri, T? payload)
        {
            var requestMessage = new HttpRequestMessage()
            {
                Method = httpMethod,
                RequestUri = uri
            };

            if (RequestHeaders != null)
                requestMessage = SetRequestHeaders(requestMessage);


            if (payload != null)
            {
                var body = JsonConvert.SerializeObject(payload);
                requestMessage.Content = new StringContent(body, Encoding.UTF8, "application/json");

                Response = Client.Send(requestMessage);

                var responseContent = JsonConvert.DeserializeObject<T>(Response.Content.ReadAsStringAsync().Result);

                return responseContent;
            }

            Response = Client.Send(requestMessage);

            return payload;
        }

        public T GetRequest<T>(string endPoint, T? body)
        {
            return SendRequest(HttpMethod.Get, new Uri($"{_baseUrl}{endPoint}"), body);
        }

        public T PostRequest<T>(string endPoint, T? body)
        {
            return SendRequest(HttpMethod.Post, new Uri($"{_baseUrl}{endPoint}"), body);
        }

        public T PutRequest<T>(string endPoint, T? body)
        {
            return SendRequest(HttpMethod.Put, new Uri($"{_baseUrl}{endPoint}"), body);
        }

        public string DeleteRequest(string endPoint)
        {
            return SendRequest<string>(HttpMethod.Delete, new Uri($"{_baseUrl}{endPoint}"), null);
        }
    }
}
