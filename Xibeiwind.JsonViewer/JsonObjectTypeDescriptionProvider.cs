using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Xibeiwind.JsonViewer
{
    public class JsonObjectTypeDescriptionProvider : TypeDescriptionProvider
    {
        public override ICustomTypeDescriptor GetTypeDescriptor(Type objectType, object instance)
        {
            return new JsonTreeObjectTypeDescriptor((JsonObject)instance);

        }
    }

    public class JsonTreeObjectTypeDescriptor : CustomTypeDescriptor, ICustomTypeDescriptor
    {
        private JsonObject JsonObject { get; }
        private PropertyDescriptorCollection PropertyCollection { get; set; }

        public JsonTreeObjectTypeDescriptor(JsonObject jsonObject)
        {
            JsonObject = jsonObject;
            InitPropertyCollection();
        }

        private void InitPropertyCollection()
        {
            var propertyDescriptors = new List<PropertyDescriptor>();

            if (JsonObject != null)
            {
                foreach (var field in JsonObject.Fields)
                {
                    PropertyDescriptor pd = new JsonTreeObjectPropertyDescriptor(field);
                    propertyDescriptors.Add(pd);
                }
            }
            PropertyCollection = new PropertyDescriptorCollection(propertyDescriptors.ToArray());

        }
    }
}