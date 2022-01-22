using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net.Http;
using System.Text;

namespace Common.Helpers
{
    public static class JsonHelper
    {
        public static T FromJsonToObject<T>(object content)
        {
            var model = JsonConvert.DeserializeObject<T>(content.ToString());
            return model;
        }
        
        public static string FromObjectToJson(object content)
        {
            var jsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                NullValueHandling = NullValueHandling.Ignore
            };

            var serializedObject = JsonConvert.SerializeObject(content, Formatting.None, jsonSerializerSettings);
            return serializedObject;
        }

        public static StringContent ToStringContent(object content)
        {
            return new StringContent(FromObjectToJson(content), Encoding.UTF8, "application/json");
        }
    }
}