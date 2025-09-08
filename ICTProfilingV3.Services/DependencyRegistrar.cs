using ICTProfilingV3.Core.Common;
using ICTProfilingV3.Interfaces;
using ICTProfilingV3.Mapper.Configurations;
using ICTProfilingV3.Repository;
using ICTProfilingV3.Services.ApiUsers;
using ICTProfilingV3.Services.Base;
using ICTProfilingV3.Services.Employees;
using Microsoft.Extensions.DependencyInjection;

namespace ICTProfilingV3.Services
{
    public static class DependencyRegistrar
    {
        public static void RegisterServices(IServiceCollection services)
        {
            //mapper
            MappingConfig.RegisterMappings(services);
            services.AddSingleton<MapperInitializer>();

            //repository
            services.AddTransient(typeof(IRepository<,>), typeof(BaseRepository<,>));


            //base
            services.AddTransient(typeof(IBaseDataService<,>), typeof(BaseDataService<,>));

            //service
            services.AddSingleton(typeof(UserStore));
            services.AddTransient<IICTRoleManager, ICTRoleManager>();
            services.AddTransient<IICTUserManager, ICTUserManager>();
            services.AddTransient<ITicketRequestService, TicketRequestService>();
            services.AddTransient<IStaffService, StaffService>();
            services.AddTransient<IEquipmentService, EquipmentService>();
            services.AddTransient<IDocActionsService, DocActionsService>();
            services.AddTransient<IProcessService, ProcessService>();
            services.AddTransient<ICASService, CASService>();
            services.AddTransient<IRepairService, RepairService>();
            services.AddTransient<IComparisonService, ComparisonService>();
            services.AddTransient<ITechSpecsService, TechSpecsService>();
            services.AddTransient<IDeliveriesService, DeliveriesService>();
            services.AddTransient<ILookUpService, LookUpService>();
            services.AddTransient<IComparisonReportService, ComparisonReportService>();
            services.AddTransient<IPurchaseReqService, PurchaseReqService>();
            services.AddTransient<IPGNService, PGNService>();
            services.AddTransient<IEvaluationService, EvaluationService>();
            services.AddTransient<IPPEInventoryService, PPEInventoryService>();
            services.AddTransient(typeof(PGNDocumentsService));
            services.AddTransient<IChangeLogService, ChangeLogService>();
            services.AddTransient<IMOService, MOService>();

            //Employees Service
            services.AddTransient(typeof(OFMISService));
            services.AddTransient(typeof(HRMISService));
        }
    }
}
