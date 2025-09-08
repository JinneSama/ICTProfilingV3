using ICTProfilingV3.API.FilesApi;
using ICTProfilingV3.BaseClasses;
using ICTProfilingV3.Core.Enums;
using System.IO;
using System.Windows.Forms;
using System;
using ICTProfilingV3.Interfaces;
using System.Linq;
using Models.Entities;

namespace ICTProfilingV3.DeliveriesForms
{
    public partial class frmComparisonReportMenu : BaseForm
    {
        private readonly HTTPNetworkFolder _networkFolder;
        private readonly IComparisonService _comparisonService;
        private int _deliveriesId;
        public frmComparisonReportMenu(HTTPNetworkFolder networkFolder, IComparisonService comparisonService)
        {
            _comparisonService = comparisonService;
            _networkFolder = networkFolder;
            InitializeComponent();
            RefreshData();
        }

        public void InitForm(int deliveriesId)
        {
            _deliveriesId = deliveriesId;
            lblTitle.Text = $"Comparison Reports for EPIS#{_deliveriesId}";
            RefreshData();
        }

        private void RefreshData()
        {
            gcComparison.DataSource = _comparisonService.GetAll().Where(x => x.DeliveriesId == _deliveriesId).ToList();
        }
        private async void simpleButton1_Click(object sender, System.EventArgs e)
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx";
                openFileDialog.Title = "Select an Excel File";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    string fileName = Path.GetFileName(filePath);

                    byte[] fileBytes = File.ReadAllBytes(filePath);

                    try
                    {
                        var res = await _comparisonService.AddAsync(new ComparisonReportFiles { DeliveriesId = _deliveriesId });
                        await _networkFolder.UploadFile(fileBytes, res.FileName, FileType.Excel);

                        RefreshData();
                        MessageBox.Show("Excel file uploaded successfully!", "Upload", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error uploading file: {ex.Message}", "Upload Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private async void btnDownload_Click(object sender, EventArgs e)
        {
            var row = (ComparisonReportFiles)gridComparison.GetFocusedRow();
            await _networkFolder.DownloadFile(row.FileName, FileType.Excel);
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            var msgRes = MessageBox.Show($"Confirm Delete this File?", "Confirmation?", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            if (msgRes == DialogResult.Cancel) return;

            var row = (ComparisonReportFiles)gridComparison.GetFocusedRow();
            await _networkFolder.DeleteFile(row.FileName);
            await _comparisonService.DeleteAsync(row.Id);
            RefreshData();
        }
    }
}