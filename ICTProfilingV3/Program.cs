using DevExpress.XtraSplashScreen;
using EntityManager.Context;
using Helpers.Interfaces;
using Helpers.Utility;
using ICTProfilingV3.API;
using ICTProfilingV3.DataTransferModels;
using ICTProfilingV3.DataTransferModels.ServiceModels.DTOModels;
using ICTProfilingV3.Dependency;
using ICTProfilingV3.Interfaces;
using ICTProfilingV3.Mapper.Configurations;
using ICTProfilingV3.Services;
using ICTProfilingV3.Services.Employees;
using ICTProfilingV3.ToolForms;
using ICTProfilingV3.Utility;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICTProfilingV3
{
    internal static class Program
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var services = new ServiceCollection();
            LoadAssemblies();
            services.AddSingleton(typeof(frmMain));
            services.AddSingleton(typeof(ApplicationDbContext));

            ControlDependencyRegistrar.RegisterControlServices(services);
            DependencyRegistrar.RegisterServices(services);
            HelpersDependencyRegistrar.RegisterServices(services);
            APIDependencyRegistrar.RegisterDependencies(services);

            ISingleInstance instanceGuard = new SingelInstance("EpisV3");

            if (!instanceGuard.IsSingleInstance())
            {
                instanceGuard.ShowDuplicateInstanceWarning();
                return;
            }

            string filePath = Path.Combine(Path.GetTempPath(), "credentials.json");
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                var credentials = JsonConvert.DeserializeObject<ArgumentCredentialsDto>(json);
                //UserStore.ArugmentCredentialsDto = credentials;
                //File.Delete(filePath);
            }

            SplashScreenManager.ShowForm(typeof(frmSplashScreen));

            using (ServiceProvider provider = services.BuildServiceProvider())
            {
                var mapperInitializer = provider.GetRequiredService<MapperInitializer>();
                mapperInitializer.InitializeMappings();

                MainAsync(provider).GetAwaiter().GetResult();
                SplashScreenManager.CloseForm();

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                var mainForm = provider.GetRequiredService<frmMain>();
                var ucManager = provider.GetRequiredService<IUCManager>();
                ucManager.SetPanelControl(mainForm.mainPanel);

                Application.Run(mainForm);
            }
        }
        private static void LoadAssemblies()
        {
            //Assembly.Load("EntityManager");
            Assembly.Load("ICTProfilingV3");
            Assembly.Load("ICTProfilingV3.DataTransferModels");
            Assembly.Load("ICTProfilingV3.Utility");
            Assembly.Load("Helpers");
            Assembly.Load("ICTProfilingV3.Interfaces");
            Assembly.Load("ICTProfilingV3.Services");
            Assembly.Load("ICTProfilingV3.Mapper");
        }

        private static async Task MainAsync(ServiceProvider provider)
        {
            var hrmisService = provider.GetRequiredService<HRMISService>();
            var ofmisService = provider.GetRequiredService<OFMISService>();

            EmployeeProviderAccessor.Provider = new HRMISEmployeeProvider();

            FDTSData.InitData();
            await HRMISEmployees.InitContext(hrmisService);
            await OFMISEmployees.InitEmployees(ofmisService);
            OFMISUsers.InitUsers(ofmisService);
        }
    }
}