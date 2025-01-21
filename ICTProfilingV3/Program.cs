﻿using DevExpress.XtraSplashScreen;
using ICTMigration.ICTv2Models;
using ICTMigration.ModelMigrations;
using ICTMigration.PPEMigration;
using ICTProfilingV3.ToolForms;
using Models.HRMISEntites;
using Models.OFMISEntities;
using Models.Repository;
using System;
using System.Configuration;
using System.Linq;
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
            SplashScreenManager.ShowForm(typeof(frmSplashScreen));
            MainAsync().GetAwaiter().GetResult();
            SplashScreenManager.CloseForm();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain());
        }

        private static async Task MainAsync()
        {
            await HRMISEmployees.InitContext();
            await OFMISEmployees.InitEmployees();
            OFMISUsers.InitUsers();

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

                PGNMigration pgnMigration = new PGNMigration();
                await pgnMigration.MigrateNonEmployee();
                await pgnMigration.MigratePGNAccounts();

                MigratePPE ppe = new MigratePPE();
                await ppe.FixMigratedPPEEmployee();
            }
        }
    }
}