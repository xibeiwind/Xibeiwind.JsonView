using Newtonsoft.Json.Linq;

namespace Xibeiwind.JsonViewer
{
    public class JsonObject
    {
        public JsonObject()
        {
            Fields = new JsonFields(this);
        }

        public JsonFields Fields { get; set; }
        public string Id { get; set; }
        public object Value { get; set; }
        public JsonType JsonType { get; set; }

        public string Text
        {
            get
            {
                if (JsonType == JsonType.Value)
                {
                    var val = Value == null ? "<null>" : Value.ToString();
                    if (Value is string)
                    {
                        val = $"\"{val}\"";
                    }

                    return val;
                }
                else
                {
                    return Id;
                }
            }
        }

        public JsonObject Parent { get; set; }

        public JToken ToJSON()
        {
            switch (JsonType)
            {
                case JsonType.Object:
                {
                    var tmp = new JObject();
                    foreach (var field in Fields)
                    {
                        tmp.Add(field.ToJSON());
                    }

                    return tmp;
                }
                case JsonType.Array:
                {
                    var tmp = new JArray();
                    foreach (var field in Fields)
                    {
                        tmp.Add(field.ToJSON());
                    }

                    return tmp;
                }
                case JsonType.Value:
                    return Value as JValue;
            }

            return default;
        }

        public bool ContainsFields(params string[] ids)
        {
            foreach (var id in ids)
            {
                if (!Fields.ContainId(id))
                {
                    return false;
                }
            }

            return true;
        }

        public bool ContainsField(string id, JsonType type)
        {
            var field = Fields[id];
            return field?.JsonType == type;
        }

        public void Modified()
        {
        }
    }
}