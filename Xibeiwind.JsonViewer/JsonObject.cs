using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Xibeiwind.JsonViewer
{
    //[DebuggerDisplay("Type={GetType().Name} Text = {Text}")]
    //[TypeConverter(typeof(ExpandableObjectConverter))]
    //public class JsonObject
    //{

    //    static JsonObject()
    //    {
    //        TypeDescriptor.AddProvider(new JsonObjectTypeDescriptionProvider(), typeof(JsonObject));
    //    }
    //    public JsonObject()
    //    {
    //        Fields = new JsonFields(this);
    //    }

    //    public JsonFields Fields { get;  }
    //    public string Id { get; set; }
    //    public object Value { get; set; }
    //    public JsonType JsonType { get; set; }

    //    public string Text
    //    {
    //        get
    //        {
    //            if (JsonType == JsonType.Value)
    //            {
    //                var val = Value == null ? "<null>" : Value.ToString();
    //                if (Value is string)
    //                {
    //                    val = $"\"{val}\"";
    //                }

    //                return $"{Id}: {val}";
    //            }
    //            else
    //            {
    //                return Id;
    //            }
    //        }
    //    }

    //    //public JsonObject Parent { get; set; }

    //    public JToken ToJSON()
    //    {
    //        switch (JsonType)
    //        {
    //            case JsonType.Object:
    //            {
    //                var tmp = new JObject();
    //                foreach (var field in Fields)
    //                {
    //                    tmp.Add(field.ToJSON());
    //                }

    //                return tmp;
    //            }
    //            case JsonType.Array:
    //            {
    //                var tmp = new JArray();
    //                foreach (var field in Fields)
    //                {
    //                    tmp.Add(field.ToJSON());
    //                }

    //                return tmp;
    //            }
    //            case JsonType.Value:
    //                return Value as JValue;
    //        }

    //        return default;
    //    }

    //    public bool ContainsFields(params string[] ids)
    //    {
    //        return ids.All(id => Fields.ContainId(id));

    //        //foreach (var id in ids)
    //        //{
    //        //    if (!Fields.ContainId(id))
    //        //    {
    //        //        return false;
    //        //    }
    //        //}

    //        //return true;
    //    }

    //    public bool ContainsField(string id, JsonType type)
    //    {
    //        var field = Fields[id];
    //        return field?.JsonType == type;
    //    }

    //    private void Modified()
    //    {
    //        //Text = null;
    //    }
    //}

    [DebuggerDisplay("Type={GetType().Name} Text = {Text}")]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class JsonObject
    {
        private string _text;

        static JsonObject()
        {
            TypeDescriptor.AddProvider(new JsonObjectTypeDescriptionProvider(), typeof(JsonObject));
        }

        public JsonObject()
        {
            Fields = new JsonFields(this);
        }

        public string Id { get; set; }

        public object Value { get; set; }

        public JsonType JsonType { get; set; }

        public JsonObject Parent { get; set; }

        public string Text
        {
            get
            {
                if (_text == null)
                {
                    if (JsonType == JsonType.Value)
                    {
                        var val = Value == null ? "<null>" : Value.ToString();
                        if (Value is string)
                            val = "\"" + val + "\"";
                        _text = $"{Id} : {val}";
                    }
                    else
                    {
                        _text = Id;
                    }
                }

                return _text;
            }
        }

        public JsonFields Fields { get; }

        public JToken ToJSON()
        {
            if (JsonType == JsonType.Object)
            {
                var tmp = new JObject();
                foreach (var field in Fields) tmp.Add(field.Id, field.ToJSON());
                return tmp;
            }

            if (JsonType == JsonType.Array)
            {
                var tmp = new JArray();
                // todo:

                //var arr = _value as JArray;
                //var arr = _value as 

                foreach (var field in Fields) tmp.Add(field.ToJSON());

                return tmp;
            }

            if (JsonType == JsonType.Value)
            {
                //return new JValue(this.Value);
                return Value as JValue;
            }

            {
                return null;
            }
        }

        internal void Modified()
        {
            _text = null;
        }

        public bool ContainsFields(params string[] ids)
        {
            foreach (var s in ids)
                if (!Fields.ContainId(s))
                    return false;
            return true;
        }

        public bool ContainsField(string id, JsonType type)
        {
            var field = Fields[id];
            return field != null && field.JsonType == type;
        }
    }

}