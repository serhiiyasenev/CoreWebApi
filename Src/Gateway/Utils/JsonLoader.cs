using Newtonsoft.Json;
using System;
using System.IO;

namespace Gateway.Utils
{
    public class JsonLoader
    {
        public static T LoadFromFile<T>(string filePath)
        {
            using var reader = new StreamReader(filePath);
            var json = reader.ReadToEnd();
            var result = JsonConvert.DeserializeObject<T>(json);
            return result;
        }

        public static T Deserialize<T>(object jsonObject)
        {
            return JsonConvert.DeserializeObject<T>(Convert.ToString(jsonObject) ?? string.Empty);
        }
    }
}
