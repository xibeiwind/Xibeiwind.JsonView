using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Newtonsoft.Json;
using Xibeiwind.JsonViewer.Properties;

namespace Xibeiwind.JsonViewer
{
    public partial class JsonViewer : UserControl
    {
        private string _json;
        private bool _updating;
        private Control LastVisualizerControl { get; set; }
        private readonly PluginsManager _pluginsManager = new PluginsManager();

        public JsonViewer()
        {
            InitializeComponent();
            try
            {
                _pluginsManager.Initialize();
            }
            catch (Exception e)
            {
                MessageBox.Show(string.Format(Resources.ConfigMessage, e.Message), "Json Viewer", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            txtJson.AllowDrop = true;
            txtJson.DragEnter += txtJson_DragEnter;
            txtJson.DragDrop += txtJson_DragDrop;
        }


        //public string Json
        //{
        //    get => txtJson.Text;
        //    set => txtJson.Text = value;
        //}
        public string Json
        {
            get => _json;
            set
            {
                if (_json != value)
                {
                    _json = value.Trim();
                    txtJson.Text = _json;
                    Redraw();
                }
            }
        }


        private void txtJson_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetData(DataFormats.FileDrop) is Array arrayFileName)
            {
                var strFileName = arrayFileName.GetValue(0).ToString();
                if (strFileName.EndsWith(".json", StringComparison.InvariantCultureIgnoreCase))
                    Json = File.ReadAllText(strFileName, Encoding.UTF8);
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
                try
                {
                    tvJson.BeginUpdate();
                    Reset();
                    if (!string.IsNullOrEmpty(Json))
                    {
                        var tree = JsonObjectTree.Parse(Json);
                        VisualizeJsonTree(tree);
                    }
                }
                finally
                {
                    tvJson.EndUpdate();
                }
            }
            catch (JsonParseErrorException e)
            {
                GetParseErrorDetails(e);
            }
            catch (Exception e)
            {
                ShowException(e);
            }
        }

        private void GetParseErrorDetails(Exception exception)
        {
        }

        private void ShowException(Exception e)
        {
            MessageBox.Show(this, e.Message, "Json Viewer", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void VisualizeJsonTree(JsonObjectTree tree)
        {
            AddNode(tvJson.Nodes, tree.Root);
            var node = GetRootNode();
            InitVisualizers(node);
            tvJson.SelectedNode = node;
            //throw new NotImplementedException();
        }

        private void InitVisualizers(JsonViewerTreeNode node)
        {
            if (!node.Initialized)
            {
                node.Initialized = true;
                var jsonObject = node.JsonObject;
                foreach (var textVisualizer in _pluginsManager.TextVisualizers)
                {
                    if (textVisualizer.CanVisualize(jsonObject))
                    {
                        node.TextVisualizers.Add(textVisualizer);
                    }
                }
                node.RefreshText();
                foreach (var visualizer in _pluginsManager.Visualizers)
                {
                    if (visualizer.CanVisualize(jsonObject))
                    {
                        node.Visualizers.Add(visualizer);
                    }
                }
            }
        }

        private JsonViewerTreeNode GetRootNode()
        {
            if (tvJson.Nodes.Count > 0) return (JsonViewerTreeNode)tvJson.Nodes[0];

            return null;
        }

        private void AddNode(TreeNodeCollection nodes, JsonObject jsonObject)
        {
            var newNode = new JsonViewerTreeNode(jsonObject);
            nodes.Add(newNode);
            newNode.Text = jsonObject.Text;
            newNode.Tag = jsonObject;
            newNode.ImageIndex = (int)jsonObject.JsonType;
            newNode.SelectedImageIndex = newNode.ImageIndex;
            foreach (var field in jsonObject.Fields)
            {
                AddNode(newNode.Nodes, field);
            }
        }

        private void btnViewSelected_Click(object sender, EventArgs e)
        {
            if (btnViewSelected.Checked)
                Json = txtJson.SelectedText.Trim();
            else
                Json = txtJson.Text.Trim();
            Redraw();
        }

        private void btnFormat_Click(object sender, EventArgs e)
        {
            try
            {
                var json = JsonConvert.SerializeObject(JsonConvert.DeserializeObject(Json), Formatting.Indented);
                //Json = json;
                txtJson.Text = json;
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
        }

        private void txtJson_TextChanged(object sender, EventArgs e)
        {
            //btnViewSelected.Checked = false;

            //try
            //{
            //    tvJson.BeginUpdate();

            //    var tree = JsonObjectTree.Parse(txtJson.Text);
            //    VisualizeJsonTree(tree);
            //}
            //finally
            //{
            //    tvJson.EndUpdate();
            //}
            Json = txtJson.Text;
            btnViewSelected.Checked = false;
        }

        private void txtJson_SelectionChanged(object sender, EventArgs e)
        {
            if (btnViewSelected.Checked)
            {
                var json = txtJson.SelectedText.Trim();
            }
        }

        private void btnPaste_Click(object sender, EventArgs e)
        {
            txtJson.Text = Clipboard.GetText();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            var text = txtJson.SelectionLength > 0 ? txtJson.SelectedText : txtJson.Text;
            Clipboard.SetText(text);
        }

        private void btnStripToCurly_Click(object sender, EventArgs e)
        {
            StripTextTo('{', '}');
        }

        private void StripTextTo(char sChr, char eChr)
        {
            var text = txtJson.Text;
            var start = text.IndexOf(sChr);
            var end = text.LastIndexOf(eChr);
            var newLen = end - start + 1;
            if (newLen > 1) txtJson.Text = text.Substring(start, newLen);
        }

        private void btnStripToSqr_Click(object sender, EventArgs e)
        {
            StripTextTo('[', ']');
        }

        private void removenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StripFromText('\n', '\r');
        }

        private void removeSpecialCharsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var text = txtJson.Text;
            text = text.Replace(@"\""", @"""");
            txtJson.Text = text;
        }

        private void StripFromText(params char[] chars)
        {
            var text = txtJson.Text;
            foreach (var ch in chars) text = text.Replace(ch.ToString(), "");
            txtJson.Text = text;
        }

        private void JsonViewer_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode & Keys.F) != Keys.F && e.Control) ShowFind();
        }

        private void ShowFind()
        {
            pnlFind.Visible = true;
            txtFind.Focus();
        }

        private void HideFind()
        {
            pnlFind.Visible = false;
            tvJson.Focus();
        }

        private void txtFind_TextChanged(object sender, EventArgs e)
        {
            txtFind.BackColor = SystemColors.Window;
            FindNext(true, true);
        }

        private void txtFind_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) FindNext(false, true);
            if (e.KeyCode == Keys.Escape) HideFind();
        }

        private bool HasNodes()
        {
            return tvJson.Nodes.Count > 0;
        }

        private bool IsMatchingNode(TreeNode startNode, string text)
        {
            return startNode.Text.ToLower().Contains(text);
        }

        private void FlashControl(Control control, Color color)
        {
            var prevColor = control.BackColor;
            try
            {
                control.BackColor = color;
                control.Refresh();
                Thread.Sleep(25);
            }
            finally
            {
                control.BackColor = prevColor;
                control.Refresh();
            }
        }

        private TreeNode GetNextNode(TreeNode startNode)
        {
            var next = startNode.FirstNode ?? startNode.NextNode;
            if (next == null)
            {
                while (startNode != null && next == null)
                {
                    startNode = startNode.Parent;
                    if (startNode != null)
                        next = startNode.NextNode;
                }

                if (next == null)
                {
                    next = GetRootNode();
                    FlashControl(txtFind, Color.Cyan);
                }
            }

            return next;
        }

        public TreeNode FindNext(TreeNode startNode, string text, bool includeSelected)
        {
            if (text == string.Empty)
                return startNode;

            if (includeSelected && IsMatchingNode(startNode, text))
                return startNode;

            var originalStartNode = startNode;
            startNode = GetNextNode(startNode);
            text = text.ToLower();
            while (startNode != originalStartNode)
            {
                if (IsMatchingNode(startNode, text))
                    return startNode;
                startNode = GetNextNode(startNode);
            }

            return null;
        }

        public bool FindNext(string text, bool includeSelected)
        {
            var startNode = tvJson.SelectedNode;
            if (startNode == null && HasNodes())
                startNode = GetRootNode();
            if (startNode != null)
            {
                startNode = FindNext(startNode, text, includeSelected);
                if (startNode != null)
                {
                    tvJson.SelectedNode = startNode;
                    return true;
                }
            }

            return false;
        }

        public bool FindNext(bool includeSelected)
        {
            return FindNext(txtFind.Text, includeSelected);
        }

        public void FindNext(bool includeSelected, bool fromUi)
        {
            if (!FindNext(includeSelected) && fromUi)
                txtFind.BackColor = Color.LightCoral;
        }

        private void cbVisualizers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_updating && GetSelectedTreeNode() != null)
            {
                ActivateVisualizer();
                GetSelectedTreeNode().LastVisualizer = (IJsonVisualizer)cbVisualizers.SelectedItem;
            }
        }

        private void ActivateVisualizer()
        {
            var visualizer = (IJsonVisualizer)cbVisualizers.SelectedItem;
            if (visualizer != null)
            {
                var jsonObject = GetSelectedTreeNode().JsonObject;
                var visualizerCtrl = visualizer.GetControl(jsonObject);
                if (LastVisualizerControl != visualizerCtrl)
                {
                    pnlVisualizer.Controls.Remove(LastVisualizerControl);
                    pnlVisualizer.Controls.Add(visualizerCtrl);
                    visualizerCtrl.Dock = DockStyle.Fill;
                    LastVisualizerControl = visualizerCtrl;
                }

                visualizer.Visualize(jsonObject);
            }
        }

        private JsonViewerTreeNode GetSelectedTreeNode()
        {
            return (JsonViewerTreeNode)tvJson.SelectedNode;
        }

        private void tvJson_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (_pluginsManager.DefaultVisualizer == null)
            {
                return;
            }
            cbVisualizers.BeginUpdate();
            _updating = true;
            try
            {
                if (e.Node is JsonViewerTreeNode node)
                {
                    var lastActive = node.LastVisualizer ??
                                     cbVisualizers.SelectedItem as IJsonVisualizer ??
                                     _pluginsManager.DefaultVisualizer;
                    cbVisualizers.Items.Clear();
                    cbVisualizers.Items.AddRange(node.Visualizers.ToArray());
                    var index = cbVisualizers.Items.IndexOf(lastActive);
                    cbVisualizers.SelectedIndex =
                        index != -1 ? index : cbVisualizers.Items.IndexOf(_pluginsManager.DefaultVisualizer);
                }
            }
            finally
            {
                cbVisualizers.EndUpdate();
                _updating = false;
            }
            ActivateVisualizer();
        }

        private void cbVisualizers_Format(object sender, ListControlConvertEventArgs e)
        {
            if (e.ListItem is IJsonViewerPlugin plugin)
            {
                e.Value = plugin.DisplayName;
            }
        }

        private void tvJson_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            foreach (JsonViewerTreeNode node in e.Node.Nodes)
            {
                InitVisualizers(node);
            }
        }

        private void tvJson_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F && e.Control) ShowFind();
        }
    }
}