using System;
using System.Globalization;

namespace Xibeiwind.JsonViewer
{
    internal class CustomDate : ICustomTextProvider
    {
        string IJsonViewerPlugin.DisplayName => "Date";

        bool IJsonViewerPlugin.CanVisualize(JsonObject jsonObject)
        {
            return jsonObject.ContainsFields("y", "M", "d", "h", "m", "s", "ms");
        }

        string ICustomTextProvider.GetText(JsonObject jsonObject)
        {
            var year = (int)jsonObject.Fields["y"].Value;
            var month = (int)jsonObject.Fields["M"].Value;
            var day = (int)jsonObject.Fields["d"].Value;
            var hour = (int)jsonObject.Fields["h"].Value;
            var min = (int)jsonObject.Fields["m"].Value;
            var second = (int)jsonObject.Fields["s"].Value;
            var ms = (int)jsonObject.Fields["ms"].Value;
            return new DateTime(year, month, day, hour, min, second, ms).ToString(CultureInfo.InvariantCulture);
        }
    }
}