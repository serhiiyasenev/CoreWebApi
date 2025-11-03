using Microsoft.AspNetCore.Http;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Gateway.Routing
{
    public class Destination(string uri, bool requiresAuthentication)
    {
        public string Path { get; set; } = uri;
        public bool RequiresAuthentication { get; set; } = requiresAuthentication;
        private static readonly HttpClient Client = new HttpClient();

        public Destination(string path) : this(path, false)
        {
        }

        private Destination() : this("/", false)
        {
        }

        public async Task<HttpResponseMessage> SendRequest(HttpRequest request)
        {
            string requestContent;
            await using (var receiveStream = request.Body)
            {
                using var readStream = new StreamReader(receiveStream, Encoding.UTF8);
                requestContent = await readStream.ReadToEndAsync();
            }

            using var newRequest = new HttpRequestMessage(new HttpMethod(request.Method), CreateDestinationUri(request));
            newRequest.Content = new StringContent(requestContent, Encoding.UTF8, request.ContentType);
            var response = await Client.SendAsync(newRequest);
            return response;
        }

        private string CreateDestinationUri(HttpRequest request)
        {
            var requestPath = request.Path.ToString();
            var queryString = request.QueryString.ToString();

            var endpoint = string.Empty;
            var endpointSplit = requestPath[1..].Split('/');

            if (endpointSplit.Length > 1) endpoint = endpointSplit[1];

            return Path + endpoint + queryString;
        }

    }
}
