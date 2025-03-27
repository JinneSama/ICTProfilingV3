using System.Windows.Forms;

namespace Helpers.Interfaces
{
    public interface IUCManager<TControl> where TControl : Control
    {
        void ShowUCSystemDetails(string key, TControl newControl, string[] propertiesToCopy);
        void RemoveUCSystemDetails(string key);
        void ClearCache();
        void NavigateForward();
        void NavigateBack();
        void RefreshControl();
    }
}
