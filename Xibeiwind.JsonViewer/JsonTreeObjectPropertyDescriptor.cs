using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Xibeiwind.JsonViewer
{
    public class JsonTreeObjectPropertyDescriptor : PropertyDescriptor
    {
        private JsonObject JsonObject { get; }
        private JsonObject[] JsonObjects { get; set; }

        public JsonTreeObjectPropertyDescriptor(JsonObject jsonObject)
            : base(jsonObject.Id, null)
        {
            JsonObject = jsonObject;
            //JsonObject = jsonObject;
            if (JsonObject.JsonType == JsonType.Array)
                InitJsonObject();
        }
        private void InitJsonObject()
        {
            var jsonObjectList = new List<JsonObject>();
            foreach (var field in JsonObject.Fields) jsonObjectList.Add(field);
            JsonObjects = jsonObjectList.ToArray();
        }
        public override bool CanResetValue(object component)
        {
            return true;
        }

        public override object GetValue(object component)
        {
            switch (JsonObject.JsonType)
            {
                case JsonType.Object:
                    return "JsonObject";
                case JsonType.Array:
                    return "JsonArray";
                default:
                    return JsonObject.Value;
            }
        }

        public override void ResetValue(object component)
        {
            throw new NotImplementedException();
        }

        public override void SetValue(object component, object value)
        {
            throw new NotImplementedException();
        }

        public override bool ShouldSerializeValue(object component)
        {
            return false;
        }

        public override Type ComponentType { get; } = null;

        public override bool IsReadOnly
        {
            get
            {
                if (JsonObject.JsonType == JsonType.Value)
                {
                    return false;
                }

                return true;
            }
        }

        public override Type PropertyType
        {
            get
            {
                switch (JsonObject.JsonType)
                {
                    case JsonType.Object:
                        return typeof(string);
                    case JsonType.Array:
                        return typeof(string);
                    default:
                        return JsonObject.Value == null ? typeof(string) : JsonObject.Value.GetType();
                }
            }
        }
    }
}