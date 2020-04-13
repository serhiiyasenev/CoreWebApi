using Newtonsoft.Json;

namespace StudentsApi.Helpers
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
           var json = JsonConvert.SerializeObject(content);
           return json;
        }
    }
}