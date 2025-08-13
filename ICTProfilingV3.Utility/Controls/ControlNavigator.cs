using ICTProfilingV3.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows.Forms;

namespace ICTProfilingV3.Utility.Controls
{
    public class ControlNavigator<T> : IControlNavigator<T> where T : Control
    {
        private readonly IServiceProvider _serviceProvider;
        public ControlNavigator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public void NavigateTo(Control parent, Action<T> configure = null)
        {
            var control = _serviceProvider.GetService<T>();
            control.Dock = DockStyle.Fill;
            configure?.Invoke(control);
            parent.Controls.Clear();
            parent.Controls.Add(control);
        }
    }
}
