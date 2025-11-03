using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace Common.Helpers
{
    public static class ResponseHelper
    {
        public static async Task<T> GetModelAsync<T>(this HttpResponseMessage response)
        {
            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
        }
    }
}
