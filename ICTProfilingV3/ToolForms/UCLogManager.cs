using Helpers.NetworkFolder;
using Models.Entities;
using Models.Repository;
using Newtonsoft.Json;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICTProfilingV3.ToolForms
{
    public partial class UCLogManager : DevExpress.XtraEditors.XtraUserControl
    {
        private readonly HTTPNetworkFolder _httpNetworkFolder;
        public UCLogManager()
        {
            InitializeComponent();
            _httpNetworkFolder = new HTTPNetworkFolder();
        }

        private async Task LoadData()
        {
            IUnitOfWork unitOfWork = new UnitOfWork();
            //var data = unitOfWork.LogEntriesRepo.GetAll(x => x.CreatedByUser).OrderByDescending(x => x.Date);
            //splashScreenLoad.ShowWaitForm();
            //gcLogs.DataSource = await data.ToListAsync();
            //splashScreenLoad.CloseWaitForm();
        }

        private async void UCLogManager_Load(object sender, System.EventArgs e)
        {
            await LoadData();
        }

        private void btnExportLogs_Click(object sender, System.EventArgs e)
        {
            splashScreenLoad.ShowWaitForm();
            IUnitOfWork unitOfWork = new UnitOfWork();
            var data = unitOfWork.LogEntriesRepo.GetAll(x => x.CreatedByUser).OrderByDescending(x => x.Date).ToList();

            var settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Formatting = Formatting.Indented
            };
            string filename = Guid.NewGuid().ToString();
            string path = Application.StartupPath + $@"\{filename}.json";
            string json = JsonConvert.SerializeObject(data, settings);
            File.WriteAllText(path, json);
            splashScreenLoad.CloseWaitForm();

            SaveFileDialog saveFileDialog = new SaveFileDialog()
            {
                Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*",
                Title = "Save Log Entries",
                FileName = $@"{filename}.json"
            };
            var res = saveFileDialog.ShowDialog();
            if (res == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;
                File.Move(path, filePath);
                MessageBox.Show("Log entries exported successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            //await _httpNetworkFolder.UploadJsonFile(path, $@"{filename}.json");
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            var res = MessageBox.Show("This will Delete all Log Entries from the Database, Proceed?", "Success", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res == DialogResult.Cancel) return;
            
            IUnitOfWork unitOfWork = new UnitOfWork();
            unitOfWork.LogEntriesRepo.TruncateEntity();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //optional
        }

        private void btnInfo_Click(object sender, EventArgs e)
        {
            var res = (LogEntry)gridLogs.GetFocusedRow();
            var frm = new frmJSONViewer(res);
            frm.ShowDialog();
        }
    }
}
