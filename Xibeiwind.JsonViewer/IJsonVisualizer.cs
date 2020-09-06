using System.Windows.Forms;

namespace Xibeiwind.JsonViewer
{
    public interface IJsonVisualizer : IJsonViewerPlugin
    {
        Control GetControl(JsonObject jsonObject);
        void Visualize(JsonObject jsonObject);
    }
    
}