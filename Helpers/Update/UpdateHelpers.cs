using System;
using System.Deployment.Application;

namespace Helpers.Update
{
    public class UpdateHelpers
    {
        public static ApplicationDeployment applicationDeployment;
        public static bool InstallUpdateSyncWithInfo()
        {
            UpdateCheckInfo info = null;
            if (ApplicationDeployment.IsNetworkDeployed)
            {
                applicationDeployment = ApplicationDeployment.CurrentDeployment;

                try
                {
                    info = applicationDeployment.CheckForDetailedUpdate();

                }
                catch (DeploymentDownloadException dde)
                {
                    // MessageBox.Show("The new version of the application cannot be downloaded at this time. \n\nPlease check your network connection, or try again later. Error: " + dde.Message);
                    return false;
                }
                catch (InvalidDeploymentException ide)
                {
                    // MessageBox.Show("Cannot check for a new version of the application. The ClickOnce deployment is corrupt. Please redeploy the application and try again. Error: " + ide.Message);
                    return false;
                }
                catch (InvalidOperationException ioe)
                {
                    // MessageBox.Show("This application cannot be updated. It is likely not a ClickOnce application. Error: " + ioe.Message);
                    return false;
                }

                if (info.UpdateAvailable)
                {
                    return true;
                }
                return false;
            }
            return false;
        }
    }
}
