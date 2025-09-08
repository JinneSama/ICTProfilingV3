using System;
using System.Windows.Forms;

namespace ICTProfilingV3.Interfaces
{
    public interface IControlNavigator<T> where T : Control
    {
        void NavigateTo(Control parent, Action<T> configure = null);
    }
}
