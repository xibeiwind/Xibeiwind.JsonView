using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Xibeiwind.JsonViewer
{
    public class JsonFields : IEnumerable<JsonObject>
    {
        private JsonObject Parent { get; }
        private List<JsonObject> Fields => FieldsById.Values.ToList();
        private Dictionary<string, JsonObject> FieldsById { get; } = new Dictionary<string, JsonObject>();
        public JsonFields(JsonObject jsonObject)
        {
            Parent = jsonObject;
        }

        public int Count => Fields.Count;
        public JsonObject this[string id] => FieldsById.TryGetValue(id,out var value) ? value : default;

        public JsonObject this[int id] => Fields[id];

        public IEnumerator<JsonObject> GetEnumerator()
        {
            return Fields.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool ContainId(string id)
        {
            return FieldsById.ContainsKey(id);
        }

        public void Add(JsonObject jsonObject)
        {
            jsonObject.Parent = Parent;
            FieldsById[jsonObject.Id] = jsonObject;
            Parent.Modified();
        }
    }
}