using Newtonsoft.Json;

namespace DynamicFilter.Sql.Tests
{
    public static class JsonExtentions
    {
        public static string ToJson<T>(this T item) => JsonConvert.SerializeObject(item);
        public static T FromJson<T>(this string json) => JsonConvert.DeserializeObject<T>(json);
    }
}
