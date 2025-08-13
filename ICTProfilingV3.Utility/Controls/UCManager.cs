using System.Collections.Generic;
using System.Windows.Forms;
using System;
using System.Collections.Concurrent;
using System.Reflection;
using ICTProfilingV3.Interfaces;
using DevExpress.XtraEditors;
using Microsoft.Extensions.DependencyInjection;

namespace Helpers.Utility
{
    public class UCManager : IUCManager
    {
        private readonly ConcurrentDictionary<string, Control> _ucSystemDetailsCache = new ConcurrentDictionary<string, Control>();
        private PanelControl _panelDetails;
        private readonly IServiceProvider _serviceProvider;
        private readonly List<string> _history = new List<string>();
        private const int MaxHistorySize = 10;
        private int _historyIndex = -1;

        public UCManager(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void SetPanelControl(PanelControl panelDetails)
        {
            _panelDetails = panelDetails;
        }
        public void ShowUCSystemDetails(string key, Control newControl, string[] propertiesToCopy)
        {
            if (string.IsNullOrWhiteSpace(key)) throw new ArgumentException("Key cannot be null or empty.", nameof(key));

            if (_ucSystemDetailsCache.TryGetValue(key, out Control existingControl))
            {
                CopyProperties(newControl, existingControl, propertiesToCopy);
            }
            else
            {
                newControl.Dock = DockStyle.Fill;
                _ucSystemDetailsCache[key] = newControl;
                existingControl = newControl;
            }

            UpdateHistory(key);
            DisplayControl(existingControl);
        }
        public void RefreshControl()
        {
            if (!(_historyIndex >= 0 && _historyIndex < _history.Count)) return;

            string currentKey = _history[_historyIndex];
            if (_ucSystemDetailsCache.TryRemove(currentKey, out Control existingControl))
            {
                Type controlType = existingControl.GetType();
                existingControl.Dispose();
                GC.Collect();

                Control newControl = (Control)Activator.CreateInstance(controlType);
                newControl.Dock = DockStyle.Fill;
                _ucSystemDetailsCache[currentKey] = newControl;

                DisplayControl(newControl);
            }
        }

        private void CopyProperties(Control source, Control destination, string[] propertiesToCopy)
        {
            if (propertiesToCopy == null) return;
            foreach (var propName in propertiesToCopy)
            {
                Type controlType = destination.GetType();
                var prop = controlType.GetProperty(propName);
                if (prop != null && prop.CanWrite)
                    prop.SetValue(destination, prop.GetValue(source));

                if (propName == "filterText")
                    InvokeApplyFilter(destination);
            }
        }
        private void InvokeApplyFilter(Control destination)
        {
            Type controlType = destination.GetType();
            var method = controlType.GetMethod("ApplyFilterText", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            if (method != null)
                method.Invoke(destination, null);
        }

        public void RemoveUCSystemDetails(string key)
        {
            if (!_ucSystemDetailsCache.TryRemove(key, out _)) return;

            _history.Remove(key);
            _historyIndex = Math.Min(_historyIndex, _history.Count - 1);
        }

        public void ClearCache()
        {
            _ucSystemDetailsCache.Clear();
            _history.Clear();
            _historyIndex = -1;
        }

        public void NavigateBack()
        {
            if (!(_historyIndex > 0)) return;

            _historyIndex--;
            DisplayControl(_ucSystemDetailsCache[_history[_historyIndex]]);
        }

        public void NavigateForward()
        {
            if (!(_historyIndex < _history.Count - 1)) return;

            _historyIndex++;
            DisplayControl(_ucSystemDetailsCache[_history[_historyIndex]]);
        }

        private void UpdateHistory(string key)
        {
            if (!(_historyIndex < 0 || _history[_historyIndex] != key)) return;

            if (_history.Count == MaxHistorySize)
                _history.RemoveAt(0);
            else
                _historyIndex++;

            _history.Add(key);
        }

        private void DisplayControl(Control control)
        {
            _panelDetails.Controls.Clear();
            _panelDetails.Controls.Add(control);
        }
    }
}
