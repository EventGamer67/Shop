using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Controller
{
    public class HttpClientSingletone : HttpClient
    {
        public HttpClientSingletone() { }
        private static HttpClientSingletone _instance;
        private static readonly object _lock = new object();
        public static HttpClientSingletone Instance()
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if(_instance == null)
                    {
                        _instance = new HttpClientSingletone();
                        _instance.DefaultRequestHeaders.Add("Authorization", "Bearer " + Manager.token);
                    }
                }
            }
            return _instance;
        }
    }
}
