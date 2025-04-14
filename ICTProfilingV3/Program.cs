using DevExpress.XtraSplashScreen;
using Helpers.Interfaces;
using Helpers.Security;
using Helpers.Utility;
using ICTMigration.Files;
using ICTMigration.ModelMigrations;
using ICTMigration.PPEMigration;
using ICTMigration.TicketStatusFix;
using ICTProfilingV3.ToolForms;
using Infrastructure.Cleaner;
using Models.FDTSEntities;
using Models.HRMISEntites;
using Models.Managers.User;
using Models.OFMISEntities;
using Models.Service.DTOModels;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.IO;
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
                UserStore.ArugmentCredentialsDto = credentials;
                //File.Delete(filePath);
            }

            SplashScreenManager.ShowForm(typeof(frmSplashScreen));
            MainAsync().GetAwaiter().GetResult();
            SplashScreenManager.CloseForm();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain());
        }

        private static async Task MainAsync()
        {
            FDTSData.InitData();
            await HRMISEmployees.InitContext();
            await OFMISEmployees.InitEmployees();
            OFMISUsers.InitUsers();

            if(ConfigurationManager.AppSettings["Run_Migration"] == "run")
            {
                ILogCleaner cleaner = new LogCleaner();
                await cleaner.CleanLogs();
            }

            if (ConfigurationManager.AppSettings["Run_Migration"] == "run")
            {
                LookUpMigration lookup = new LookUpMigration();
                await lookup.MigrateActionDropdowns();
                await lookup.MigrateActionList();
                await lookup.MigrateEquipments();
                await lookup.MigrateStandardPR();
                await lookup.MigrateTSBasis();

                UsersMigration usersMigration = new UsersMigration();
                await usersMigration.MigrateUsers();

                TicketMigration ticketMigration = new TicketMigration();
                await ticketMigration.MigrateTickets();
                await ticketMigration.MigrateDeliveries();
                await ticketMigration.MigrateTechSpecs();
                await ticketMigration.MigratePPE();
                await ticketMigration.MigrateRepairs();

                RecordProcessesMigration recordProcessesMigration = new RecordProcessesMigration();
                await recordProcessesMigration.MigrateCAS();
                await recordProcessesMigration.MigratePR();

                ActionMigration actionMigration = new ActionMigration();
                await actionMigration.MigrateTSActions();
                await actionMigration.MigrateDeliveriesActions();
                await actionMigration.MigrateRepairActions();
                await actionMigration.MigrateCASActions();
                await actionMigration.MigratePGNActions();

                AssignedStaffMigration assignedStaffMigration = new AssignedStaffMigration();
                await assignedStaffMigration.GetAssignedUsersDeliveries();
                await assignedStaffMigration.GetAssignedUsersRepair();
                await assignedStaffMigration.GetAssignedUsersTS();

                PGNMigration pgnMigration = new PGNMigration();
                await pgnMigration.MigrateNonEmployee();
                await pgnMigration.MigratePGNAccounts();

                MigratePPE ppe = new MigratePPE();
                await ppe.FixMigratedPPEEmployee();

                FilesMigration filesMigration = new FilesMigration();
                await filesMigration.MigratePGNImages();

                RecordsStatusFix recordsStatusFix = new RecordsStatusFix();
                await recordsStatusFix.CASFix();
                await recordsStatusFix.PRFix();
            }
        }
    }
}