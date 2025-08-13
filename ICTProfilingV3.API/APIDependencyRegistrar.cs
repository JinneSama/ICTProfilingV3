using ICTProfilingV3.API.FilesApi;
using Microsoft.Extensions.DependencyInjection;

namespace ICTProfilingV3.API
{
    public class APIDependencyRegistrar
    {
        public static void RegisterDependencies(IServiceCollection services)
        {
            services.AddSingleton<TokenCache>();
            services.AddSingleton<HTTPNetworkFolder>();
        }
    }
}
