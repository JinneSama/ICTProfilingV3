using ICTMigration.ModelMigrations;
using Models.HRMISEntites;
using Models.OFMISEntities;
using System;
using System.Configuration;
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
            HRMISEmployees.InitContext();
            OFMISEmployees.InitEmployees();

            MainAsync().GetAwaiter().GetResult();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain());
        }

        private static async Task MainAsync()
        {
            if (ConfigurationManager.AppSettings["Run_Migration"] == "run")
            {
                LookUpMigration lookup = new LookUpMigration();
                await lookup.MigrateActionDropdowns();
                await lookup.MigrateActionList();
                await lookup.MigrateEquipments();
                await lookup.MigrateStandardPR();

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

                AssignedStaffMigration assignedStaffMigration = new AssignedStaffMigration();
                await assignedStaffMigration.GetAssignedUsersDeliveries();
                await assignedStaffMigration.GetAssignedUsersRepair();
                await assignedStaffMigration.GetAssignedUsersTS();

            }
        }
    }
}