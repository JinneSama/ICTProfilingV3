using DevExpress.XtraCharts;
using ICTProfilingV3.ActionsForms;
using ICTProfilingV3.BaseClasses;
using ICTProfilingV3.CustomerActionSheetForms;
using ICTProfilingV3.DashboardForms;
using ICTProfilingV3.DeliveriesForms;
using ICTProfilingV3.EvaluationForms;
using ICTProfilingV3.MOForms;
using ICTProfilingV3.PGNForms;
using ICTProfilingV3.PPEInventoryForms;
using ICTProfilingV3.PurchaseRequestForms;
using ICTProfilingV3.RepairForms;
using ICTProfilingV3.StandardPRForms;
using ICTProfilingV3.TechSpecsForms;
using ICTProfilingV3.TicketRequestForms;
using ICTProfilingV3.ToolForms;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace ICTProfilingV3.Dependency
{
    public static class ControlDependencyRegistrar
    {
        public static void RegisterControlServices(IServiceCollection services)
        {
            services.AddTransient(typeof(UCActions));
            services.AddTransient(typeof(UCCAS));
            services.AddTransient(typeof(UCClientDashboard));
            services.AddTransient(typeof(UCDashboard));
            services.AddTransient(typeof(UCM365Dashboard));
            services.AddTransient(typeof(UCPGNDashboard));
            services.AddTransient(typeof(UCQueue));
            services.AddTransient(typeof(UCRepairDashboard));
            services.AddTransient(typeof(UCRequestDashboard));
            services.AddTransient(typeof(UCRoutedActions));
            services.AddTransient(typeof(UCUserTasks));
            services.AddTransient(typeof(UCDeliveries));
            services.AddTransient(typeof(UCDeliveriesSpecs));
            services.AddTransient(typeof(UCEvaluationSheet));
            services.AddTransient(typeof(UCMOAccounts));
            services.AddTransient(typeof(UCMOAccountUserRequests));
            services.AddTransient(typeof(UCMacAdresses));
            services.AddTransient(typeof(UCPGNAccounts));
            services.AddTransient(typeof(UCPGNRequests));
            services.AddTransient(typeof(UCPGNScanDocuments));
            services.AddTransient(typeof(UCRequestAccount));
            services.AddTransient(typeof(UCPPEs));
            services.AddTransient(typeof(UCPPEsSpecs));
            services.AddTransient(typeof(UCRepairHistory));
            services.AddTransient(typeof(UCFDTS));
            services.AddTransient(typeof(UCOFMISPR));
            services.AddTransient(typeof(UCPR));
            services.AddTransient(typeof(UCAddPPEEquipment));
            services.AddTransient(typeof(UCRepair));
            services.AddTransient(typeof(UCStandardPR));
            services.AddTransient(typeof(UCRequestedTechSpecs));
            services.AddTransient(typeof(UCTechSpecs));
            services.AddTransient(typeof(UCTARequestDashboard));
            services.AddTransient(typeof(UCAssignedTo));
            services.AddTransient(typeof(UCLogManager));
            services.AddTransient(typeof(UCAssignedTo));
            services.AddTransient(typeof(UCLogManager));

            RegisterForms(services);
        }

        public static void RegisterForms(IServiceCollection services)
        {
            var formTypes = AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(a => a.GetTypes())
                    .Where(t => (t.IsSubclassOf(typeof(BaseForm))) && !t.IsAbstract && !t.IsGenericType);

            foreach (var formType in formTypes)
            {
                services.AddTransient(formType);
            }
        }
    }
}
