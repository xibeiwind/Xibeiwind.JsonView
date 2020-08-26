using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace Xibeiwind.JsonViewer
{
    public partial class JsonViewer : UserControl
    {
        public JsonViewer()
        {
            InitializeComponent();
            
        }


        public string Json
        {
            get => txtJson.Text;
            set => txtJson.Text = value;
        }

        private void txtJson_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetData(DataFormats.FileDrop) is Array arrayFileName)
            {
                var strFileName = arrayFileName.GetValue(0).ToString();
                if (strFileName.EndsWith(".json", StringComparison.InvariantCultureIgnoreCase))
                {
                    Json = File.ReadAllText(strFileName, Encoding.UTF8);
                }
            }
        }

        private void txtJson_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.Link : DragDropEffects.None;
        }

        private void Redraw()
        {
            try
            {
                tvJson.BeginUpdate();
                Reset();
                if (!string.IsNullOrEmpty(Json))
                {
                    var tree = JsonObjectTree.Parse(Json);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private void Reset()
        {
            ClearInfo();
            tvJson.Nodes.Clear();

            pnlVisualizer.Controls.Clear();
            //_lastVisualizerControl = null;
            cbVisualizers.Items.Clear();
        }

        private void ClearInfo()
        {
            lblError.Text = string.Empty;
            
        }

        private void txtJson_TextChanged(object sender, EventArgs e)
        {
            btnViewSelected.Checked = false;
        }

        private void btnViewSelected_Click(object sender, EventArgs e)
        {
            Redraw();
        }

        private void btnFormat_Click(object sender, EventArgs e)
        {
            try
            {
                var json = JsonConvert.SerializeObject(JsonConvert.DeserializeObject(Json), Formatting.Indented);
                Json = json;
            }
            catch
            {

            }
        }
    }
}
