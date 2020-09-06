using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Xibeiwind.JsonViewer
{
    public partial class SimpleGridVisualizer : UserControl, IJsonVisualizer
    {

        public SimpleGridVisualizer()
        {
            InitializeComponent();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CopyGridToClipboard();

        }

        private void CopyGridToClipboard()
        {
            if (simpleGridView.SelectedCells.Count <= 0)
            {
                Clipboard.SetText("");
                return;
            }

            var arr = new DataGridViewCell[simpleGridView.SelectedCells.Count];
            simpleGridView.SelectedCells.CopyTo(arr, 0);

            var colIndexArr = arr.Select(a => a.ColumnIndex).Distinct().OrderBy(a => a)
                .Select(a => simpleGridView.Columns[a].HeaderText).ToArray();

            var builder = new StringBuilder();
            builder.AppendLine();
            builder.AppendLine($"\t{colIndexArr}");
            builder.AppendLine(simpleGridView.GetClipboardContent()?.GetText());
            Clipboard.SetText(builder.ToString());
        }

        string IJsonViewerPlugin.DisplayName => "Simple Grid";

        bool IJsonViewerPlugin.CanVisualize(JsonObject jsonObject) => jsonObject.JsonType == JsonType.Array;

        Control IJsonVisualizer.GetControl(JsonObject jsonObject) => this;

        void IJsonVisualizer.Visualize(JsonObject jsonObject)
        {
            var table = GetDataTable(jsonObject.Fields);
            //simpleGridView.Columns.AddRange(GetFilterableColumns(table.Columns).ToArray());
            simpleGridView.DataSource = table;

        }

        private DataTable GetDataTable(JsonFields fields)
        {
            var table = new DataTable();

            foreach (var field in fields[0].Fields)
            {
                table.Columns.Add(field.Id);
            }
            foreach (var jsonField in fields)
            {
                var row = table.NewRow();

                foreach (DataColumn col in table.Columns)
                {
                    try
                    {
                        var val = jsonField.Fields[col.ColumnName]?.Value.ToString();
                        if (int.TryParse(val, out var intVal))
                        {
                            row[col] = intVal;
                        }
                        else
                        {
                            row[col] = jsonField.Fields[col.ColumnName]?.Value;
                        }
                    }
                    catch
                    {
                        row[col] = jsonField.Fields[col.ColumnName]?.Value;
                    }
                }
                table.Rows.Add(row);
            }

            return table;
        }

        private void simpleGridView_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C) CopyGridToClipboard();

        }

        private void simpleGridView_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            if (sender is DataGridView dgv)
            {
                var rectangle = new Rectangle(e.RowBounds.Location.X,
                    e.RowBounds.Location.Y,
                    dgv.RowHeadersWidth - 4,
                    e.RowBounds.Height);

                var row = dgv.Rows[e.RowIndex];
                var color = row.Selected
                    ? dgv.RowHeadersDefaultCellStyle.SelectionForeColor
                    : dgv.RowHeadersDefaultCellStyle.ForeColor;

                TextRenderer.DrawText(e.Graphics, $"{e.RowIndex + 1}",
                    dgv.RowHeadersDefaultCellStyle.Font,
                    rectangle,
                    color,
                    TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
            }
        }
    }
}
