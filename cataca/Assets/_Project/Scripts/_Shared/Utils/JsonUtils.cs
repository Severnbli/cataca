using Newtonsoft.Json;

namespace _Project.Scripts._Shared.Utils
{
    public static class JsonUtils
    {
        public static string ToJson<T>(T obj, bool pretty = false)
        {
            return JsonConvert.SerializeObject(obj, pretty ? Formatting.Indented : Formatting.None);
        }

        public static T FromJson<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}