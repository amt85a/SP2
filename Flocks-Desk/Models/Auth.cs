using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Flocks_Desk.Models
{
    internal class Auth
    {

        // Sub class inside Auth class. Used to acces token according to the JSON responds to /login route.
        public class SuccessMessage
        {
            public string Token { get; set; }
        }

        // SuccessMessage Property (as defined by the above class).
        public SuccessMessage Success { get; set; }

        public static String Login(String email, String pwd)
        {
            // associative array with email and password
            Dictionary<String, String> userCredentials = new Dictionary<string, string>();
            userCredentials.Add("email", email);
            userCredentials.Add("password", pwd);

            // call /login route to API
            RestResponse response = Api.Post("login", userCredentials);
            Console.WriteLine(response.Content);

            // Deserialization du Json en une instance de la classe login
            Auth result = JsonSerializer.Deserialize<Auth>(response.Content, Api.jsonOptions);

            // Print response in console
            Console.WriteLine(response.Content);

            // Assign message according to response status code
            string message = "";
            switch (response.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    message = "Success";
                    Api.Token = result.Success.Token;
                    Console.WriteLine(result.Success.Token);
                    break;
                case System.Net.HttpStatusCode.Unauthorized:
                    message = "Unauthorized access. Please try again !";
                    break;
                default:
                    message = "Something wrong occured with the connection to database";
                    break;
            }
            return message;

            //   return null;
        }
    }
}
