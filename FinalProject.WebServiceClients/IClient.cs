using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.WebServiceClients
{
    public interface IClient
    {
        public T GetRequest<T>(string endPoint, T? body);

        public T PostRequest<T>(string endPoint, T? body);

        public T PutRequest<T>(string endPoint, T? body);

        public string DeleteRequest(string endPoint);

        public void AddRequestHeaders(string key, string value);

        public void AddRequestParameters(string key, string value);

        public HttpResponseMessage GetResponse();

    }
}
