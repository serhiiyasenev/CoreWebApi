using Common.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Gateway.Routing
{
    public class Router
    {
        public List<Route> Routes { get; set; }
        public Destination AuthenticationService { get; set; }

        public Router(string routeConfigFilePath)
        {
            var router = JsonHelper.LoadFromFile<dynamic>(routeConfigFilePath);

            Routes = JsonHelper.Deserialize<List<Route>>(Convert.ToString(router.routes));
            AuthenticationService = JsonHelper.Deserialize<Destination>(Convert.ToString(router.authenticationService));
        }

        public async Task<HttpResponseMessage> RouteRequest(HttpRequest request)
        {
            var path = request.Path.ToString();
            var basePath = '/' + path.Split('/')[1];

            Destination destination;

            try
            {
                destination = Routes.First(r => r.Endpoint.Equals(basePath)).Destination;
            }
            catch
            {
                return ConstructErrorMessage("The path could not be found.");
            }

            if (!destination.RequiresAuthentication) return await destination.SendRequest(request);

            var token = request.Headers["token"];
            var keyValuePairs = request.Query.Append(new KeyValuePair<string, StringValues>("token", token));
            keyValuePairs.ToList().ForEach(e =>
            {
                // just to test
                var (key, value) = e;
                var result = string.Join(';', key, value);
                Console.WriteLine(result);
            });

            using var authResponse = await AuthenticationService.SendRequest(request);

            if (!authResponse.IsSuccessStatusCode) return ConstructErrorMessage("Authentication failed.");

            return await destination.SendRequest(request);
        }

        private static HttpResponseMessage ConstructErrorMessage(string error)
        {
            var errorMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.NotFound,
                Content = new StringContent(error)
            };
            return errorMessage;
        }
    }
}
