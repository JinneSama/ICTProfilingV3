using ICTProfilingV3.Interfaces;
using ICTProfilingV3.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace ICTProfilingV3.Services
{
    public static class DependencyRegistrar
    {
        public static void RegisterServices(IServiceCollection services)
        {
            //repository
            services.AddTransient(typeof(IRepository<,>), typeof(BaseRepository<,>));

            //service
            services.AddTransient<ITicketRequestService, TicketRequestService>();
        }
    }
}
