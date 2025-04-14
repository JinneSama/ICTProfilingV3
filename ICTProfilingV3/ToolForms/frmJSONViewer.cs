using Models.Entities;
using Newtonsoft.Json.Linq;
using System;
using System.Windows.Forms;

namespace ICTProfilingV3.ToolForms
{
    public partial class frmJSONViewer : DevExpress.XtraEditors.XtraForm
    {
        private readonly LogEntry _logEntry;
        public frmJSONViewer(LogEntry entry)
        {
            InitializeComponent();
            _logEntry = entry;
        }

        private void LoadJSON(string jsonText)
        {
            try
            {
                treeViewJSON.Nodes.Clear();
                string json = jsonText;

                var parsedJson = JToken.Parse(json);
                TreeNode rootNode = new TreeNode("JSON");
                treeViewJSON.Nodes.Add(rootNode);
                PopulateTree(parsedJson, rootNode);

                treeViewJSON.ExpandAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid JSON: " + ex.Message);
            }
        }
        private void PopulateTree(JToken token, TreeNode node)
        {
            if (token is JValue value)
            {
                node.Text += ": " + value.ToString();
            }
            else if (token is JObject obj)
            {
                foreach (var property in obj.Properties())
                {
                    TreeNode child = new TreeNode(property.Name);
                    node.Nodes.Add(child);
                    PopulateTree(property.Value, child);
                }
            }
            else if (token is JArray array)
            {
                for (int i = 0; i < array.Count; i++)
                {
                    TreeNode child = new TreeNode($"[{i}]");
                    node.Nodes.Add(child);
                    PopulateTree(array[i], child);
                }
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            LoadJSON(_logEntry.NewValues);
        }

        private void btnOld_Click(object sender, EventArgs e)
        {
            LoadJSON(_logEntry.OldValues);
        }
    }
}