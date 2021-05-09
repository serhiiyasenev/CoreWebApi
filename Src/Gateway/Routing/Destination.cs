using Microsoft.AspNetCore.Http;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Gateway.Routing
{
    public class Destination
    {
        public string Path { get; set; }
        public bool RequiresAuthentication { get; set; }
        static HttpClient client = new HttpClient();

        public Destination(string uri, bool requiresAuthentication)
        {
            Path = uri;
            RequiresAuthentication = requiresAuthentication;
        }

        public Destination(string path) : this(path, false)
        {
        }

        private Destination()
        {
            Path = "/";
            RequiresAuthentication = false;
        }

        public async Task<HttpResponseMessage> SendRequest(HttpRequest request)
        {
            string requestContent;
            await using (var receiveStream = request.Body)
            {
                using var readStream = new StreamReader(receiveStream, Encoding.UTF8);
                requestContent = await readStream.ReadToEndAsync();
            }

            using var newRequest = new HttpRequestMessage(new HttpMethod(request.Method), CreateDestinationUri(request))
            {
                Content = new StringContent(requestContent, Encoding.UTF8, request.ContentType)
            };
            using var response = await client.SendAsync(newRequest);
            return response;
        }

        private string CreateDestinationUri(HttpRequest request)
        {
            var requestPath = request.Path.ToString();
            var queryString = request.QueryString.ToString();

            var endpoint = "";
            var endpointSplit = requestPath.Substring(1).Split('/');

            if (endpointSplit.Length > 1)
                endpoint = endpointSplit[1];

            return Path + endpoint + queryString;
        }

    }
}
