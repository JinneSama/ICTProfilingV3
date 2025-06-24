using DevExpress.XtraSplashScreen;
using EntityManager.Context;
using Helpers.Interfaces;
using Helpers.Security;
using Helpers.Utility;
using ICTMigration.Files;
using ICTMigration.ModelMigrations;
using ICTMigration.PPEMigration;
using ICTMigration.TicketStatusFix;
using ICTProfilingV3.Services;
using ICTProfilingV3.TicketRequestForms;
using ICTProfilingV3.ToolForms;
using Infrastructure.Cleaner;
using Microsoft.Extensions.DependencyInjection;
using Models.FDTSEntities;
using Models.HRMISEntites;
using Models.Managers.User;
using Models.OFMISEntities;
using Models.Service.DTOModels;
using Newtonsoft.Json;
using System;
using System.Configuration;
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

            services.AddTransient(typeof(ApplicationDbContext));
            services.AddTransient(typeof(frmMain));
            services.AddTransient(typeof(UCTARequestDashboard));

            DependencyRegistrar.RegisterServices(services);
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
            MainAsync().GetAwaiter().GetResult();
            SplashScreenManager.CloseForm();

            using (ServiceProvider provider = services.BuildServiceProvider())
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                var mainForm = provider.GetRequiredService<frmMain>();
                Application.Run(mainForm);
            }
        }
        private static void LoadAssemblies()
        {
            //Assembly.Load("EntityManager");
            Assembly.Load("Helpers");
            Assembly.Load("ICTMigration");
            Assembly.Load("ICTProfilingV3");
            //Assembly.Load("ICTProfilingV3.Interfaces");
            //Assembly.Load("ICTProfilingV3.Services");
            Assembly.Load("Infrastructure");
            Assembly.Load("Mapper");
        }

        private static async Task MainAsync()
        {
            FDTSData.InitData();
            await HRMISEmployees.InitContext();
            await OFMISEmployees.InitEmployees();
            OFMISUsers.InitUsers();
        }
    }
}