using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Authenticators.OAuth2;

namespace Flocks_Desk.Models
{
    internal class Api
    {
        private static String _url = Properties.Resources.API_URL;
        private static String _version = Properties.Resources.API_VERSION;
        public static String Token { get; set; }
        public static JsonSerializerOptions jsonOptions = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

        public static RestResponse Post(String route, Dictionary<String, String> fields = null)
        {
            // HTTP instance object where to send the request
            RestClient clientApi = new RestClient(Api._url + Api._version);

            // Prepare request

            RestRequest request = new RestRequest(route, Method.Post);
            request.Resource = route;
            request.AddJsonBody(fields);

            // send HTTP request and return response
            return clientApi.Execute(request);

        }
        public static RestResponse GetWithToken(String route, Dictionary<String, String> fields = null)
        {
#if DEBUG
            Console.WriteLine("Api - GetWithToken");
#endif
            OAuth2AuthorizationRequestHeaderAuthenticator authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(Token, "Bearer");

            RestClientOptions options = new RestClientOptions(Api._url + Api._version)
            {
                Authenticator = authenticator
            };
            RestClient clientApi = new RestClient(options);

            //Prepare request
            RestRequest request = new RestRequest(route, Method.Get);

            // Just include necessary field (otherwise request will fail)
            if (null != fields)
            {
                foreach (KeyValuePair<String, String> field in fields)
                {
                    request.AddUrlSegment(field.Key, field.Value);
                }

            }

            // send HTTP request and return response
            return clientApi.Execute(request);


        }
    }
}
