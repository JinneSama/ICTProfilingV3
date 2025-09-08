using ICTMigration.Files;
using ICTMigration.ModelMigrations;
using ICTMigration.PPEMigration;
using ICTMigration.TicketStatusFix;
using Infrastructure.Cleaner;
using System.Configuration;
using System.Threading.Tasks;

namespace ICTMigration
{
    public class MigrateEntities
    {
        public async Task Migrate()
        {
            if (ConfigurationManager.AppSettings["Run_Migration"] == "run")
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
