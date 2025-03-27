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
                catch (DeploymentDownloadException)
                {
                    return false;
                }
                catch (InvalidDeploymentException)
                {
                    return false;
                }
                catch (InvalidOperationException)
                {
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
