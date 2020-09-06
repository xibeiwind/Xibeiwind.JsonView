using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Xibeiwind.JsonViewer
{
    public class JsonViewerTreeNode : TreeNode
    {
        public JsonViewerTreeNode(JsonObject jsonObject)
        {
            JsonObject = jsonObject;
        }

        public bool Initialized { get; set; }
        public JsonObject JsonObject { get; set; }

        public List<ICustomTextProvider> TextVisualizers { get; } = new List<ICustomTextProvider>();

        public List<IJsonVisualizer> Visualizers { get; } = new List<IJsonVisualizer>();

        public IJsonVisualizer LastVisualizer { get; set; }

        public void RefreshText()
        {
            var builder = new StringBuilder(JsonObject.Text);
            foreach (var provider in TextVisualizers)
                try
                {
                    var customText = provider.GetText(JsonObject);
                    builder.Append($"({customText})");
                }
                catch
                {
                    // ignored
                }

            var text = builder.ToString();
            if (text != Text) Text = text;
        }
    }
}