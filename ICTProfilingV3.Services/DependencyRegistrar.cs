using ICTProfilingV3.Core.Common;
using ICTProfilingV3.Interfaces;
using ICTProfilingV3.Mapper;
using ICTProfilingV3.Mapper.Configurations;
using ICTProfilingV3.Repository;
using ICTProfilingV3.Services.ApiUsers;
using ICTProfilingV3.Services.Employees;
using Microsoft.Extensions.DependencyInjection;
using System;

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

            //Employees Service
            services.AddTransient(typeof(OFMISService));
            services.AddTransient(typeof(HRMISService));
        }
    }
}
