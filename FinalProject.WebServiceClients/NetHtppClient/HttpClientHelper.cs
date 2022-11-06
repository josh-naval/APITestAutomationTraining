using Newtonsoft.Json;
using System.Text;

namespace FinalProject.WebServiceClients.NetHttpClient
{
    public class HttpClientHelper: IClient
    {
        public Dictionary<string, string> RequestHeaders { get; set; }

        public Dictionary<string, string> RequestParameters { get; set; }

        private static HttpClient _client = new();

        private HttpResponseMessage _response;

        private readonly string _baseUrl;

        public HttpClientHelper(string baseUrl)
        {
            _baseUrl = baseUrl;
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

        private HttpRequestMessage SetRequestHeaders(HttpRequestMessage requestMessage)
        {
            foreach (var requestHeader in RequestHeaders!)
            {
                requestMessage.Headers.Add(requestHeader.Key, requestHeader.Value);
            }

            return requestMessage;
        }

        public T SendRequest<T>(HttpMethod httpMethod, Uri uri, T payload)
        {
            try
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
                    var body = JsonConvert.SerializeObject(payload, Formatting.Indented);
                    requestMessage.Content = new StringContent(body, Encoding.UTF8, "application/json");

                    _response = _client.Send(requestMessage);

                    var responseContent = JsonConvert.DeserializeObject<T>(_response.Content.ReadAsStringAsync().Result);

                    return responseContent;
                }

                _response = _client.Send(requestMessage);

                // WORK TO DO: Line 79
                // Remarks: Temporary fix. probably need to remove deserilization from helper and just return the response
                // which will cause a huge refactor on this code.

                if (httpMethod == HttpMethod.Get || httpMethod == HttpMethod.Put)
                    return JsonConvert.DeserializeObject<T>(_response.Content.ReadAsStringAsync().Result);

                return payload;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return (T)Activator.CreateInstance(typeof(T));
            }
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

        public HttpResponseMessage GetResponse()
        {
            return _response;
        }
    }
}