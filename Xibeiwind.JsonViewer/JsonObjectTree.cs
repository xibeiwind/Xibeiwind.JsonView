using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Xibeiwind.JsonViewer
{
    public class JsonObjectTree
    {
        public JsonObjectTree(JObject rootObject)
        {
            Root = ConvertToObject("JSON", rootObject);
        }

        public JsonObject Root { get; }

        public static JsonObjectTree Parse(string json)
        {
            var obj = JsonConvert.DeserializeObject<JObject>(json);
            return new JsonObjectTree(obj);
        }

        private JsonObject ConvertToObject(string id, JToken jsonObject)
        {
            var obj = CreateJObject(jsonObject);
            obj.Id = id;
            AddChildren(jsonObject, obj);
            return obj;
        }

        private void AddChildren(JToken jsonObject, JsonObject obj)
        {
            if (jsonObject is JObject jObject)
            {
                foreach (var property in jObject.Properties())
                {
                    obj.Fields.Add(ConvertToObject(property.Name, property.Value));
                }
            }
            else
            {
                if (jsonObject is JArray arr)
                {
                    for (int i = 0; i < arr.Count; i++)
                    {
                        obj.Fields.Add(ConvertToObject($"[{i}]", arr[i]));
                    }
                }
            }
        }

        private JsonObject CreateJObject(object jsonObject)
        {
            var result = new JsonObject();

            switch (jsonObject)
            {
                case JArray _:
                    result.JsonType = JsonType.Array;
                    break;
                case JObject _:
                    result.JsonType = JsonType.Object;
                    break;
                default:
                    result.JsonType = JsonType.Value;
                    result.Value = jsonObject;
                    break;
            }

            return result;
        }
    }

    public enum JsonType
    {
        Object,
        Array,
        Value
    }
}