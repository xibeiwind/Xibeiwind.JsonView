using System.Collections.Generic;
using System.Windows.Forms;

namespace Xibeiwind.JsonViewer
{
    public partial class GridVisualizer : UserControl, IJsonVisualizer
    {

        public GridVisualizer()
        {
            InitializeComponent();
        }

        string IJsonViewerPlugin.DisplayName => "Grid";

        bool IJsonViewerPlugin.CanVisualize(JsonObject jsonObject)
        {
            return jsonObject.ContainsField("headers", JsonType.Array) &&
                   jsonObject.ContainsField("rows", JsonType.Array);
        }

        Control IJsonVisualizer.GetControl(JsonObject jsonObject) => this;

        void IJsonVisualizer.Visualize(JsonObject jsonObject)
        {
            lvGrid.BeginUpdate();
            try
            {
                lvGrid.Columns.Clear();
                lvGrid.Items.Clear();
                FillHeaders(jsonObject.Fields["headers"]);
                FillRows(jsonObject.Fields["rows"]);
            }
            finally
            {
                lvGrid.EndUpdate();
            }
        }

        private void FillHeaders(JsonObject jsonObject)
        {
            foreach (var header in jsonObject.Fields)
            {
                var nameHeader = header.Fields["name"];
                if (nameHeader.JsonType == JsonType.Value && nameHeader.Value is string name)
                {
                    lvGrid.Columns.Add(name);
                }
            }
        }

        private void FillRows(JsonObject jsonObject)
        {
            foreach (var row in jsonObject.Fields)
            {
                var rowValues = new List<string>();
                foreach (var rowValue in row.Fields)
                {
                    if (rowValue.JsonType == JsonType.Value && rowValue.Value is string value)
                    {

                    }
                    else
                    {
                        value = string.Empty;
                    }
                    rowValues.Add(value);
                }

                var rowItem = new ListViewItem(rowValues.ToArray());
                lvGrid.Items.Add(rowItem);
            }
        }
    }
}
