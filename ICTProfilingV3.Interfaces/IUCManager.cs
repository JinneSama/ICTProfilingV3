using DevExpress.XtraEditors;
using System.Windows.Forms;

namespace ICTProfilingV3.Interfaces
{
    public interface IUCManager
    {
        void SetPanelControl(PanelControl panelDetails);
        void ShowUCSystemDetails(string key, Control newControl, string[] propertiesToCopy);
        void RemoveUCSystemDetails(string key);
        void ClearCache();
        void NavigateForward();
        void NavigateBack();
        void RefreshControl();
    }
}
