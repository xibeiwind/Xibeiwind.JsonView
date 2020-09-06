using System;

namespace Xibeiwind.JsonViewer
{
    public class AjaxNetDateTime : ICustomTextProvider
    {

        private static readonly long Epoch = new DateTime(1970, 1, 1).Ticks;
        string IJsonViewerPlugin.DisplayName => "Ajax.Net DateTime";

        bool IJsonViewerPlugin.CanVisualize(JsonObject jsonObject)
        {
            if (jsonObject.JsonType == JsonType.Value && jsonObject.Value is string text)
            {
                return text.Length > 2 && text[0] == '@' && text[text.Length - 1] == '@';
            }

            return false;
        }

        string ICustomTextProvider.GetText(JsonObject jsonObject)
        {
            var text = (string)jsonObject.Value;
            return $"Ajax.Net Date: {ConvertJSTicksToDateTime(Convert.ToInt64(text.Substring(1, text.Length - 2)))}";
        }

        private DateTime ConvertJSTicksToDateTime(long ticks)
        {

            return new DateTime(ticks * 1000 + Epoch);
        }
    }
}
