using Helpers.Interfaces;
using System;
using System.Threading;
using System.Windows.Forms;

namespace Helpers.Utility
{
    public class SingelInstance : ISingleInstance
    {
        private static Mutex mutex;
        private readonly string _appId;
        public SingelInstance(string appId)
        {
            _appId = appId;
        }
        public void ReleaseInstance()
        {
            throw new System.NotImplementedException();
        }

        public bool IsSingleInstance()
        {
            bool createdNew;
            mutex = new Mutex(true, _appId, out createdNew);
            return createdNew;
        }
        public void ShowDuplicateInstanceWarning()
        {
            MessageBox.Show("The application is already running.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
