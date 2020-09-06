using System;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace Xibeiwind.JsonViewer
{
    public partial class JsonObjectVisualizer : UserControl, IJsonVisualizer
    {

        public JsonObjectVisualizer()
        {
            InitializeComponent();
        }

        string IJsonViewerPlugin.DisplayName => "Property Grid";

        bool IJsonViewerPlugin.CanVisualize(JsonObject jsonObject) => true;

        Control IJsonVisualizer.GetControl(JsonObject jsonObject) => this;

        void IJsonVisualizer.Visualize(JsonObject jsonObject)
        {
            this.Invoke(() =>
            {
                pgJsonObject.SelectedObject = jsonObject;
                //label1.Text = JsonConvert.SerializeObject(jsonObject, Formatting.Indented);
            });

        }
    }

    public static class ControlExtensions
    {
        public static void Invoke(this Control control, Action action)
        {
            control.Invoke(action);

        }
    }
}
