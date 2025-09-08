using ICTProfilingV3.API.FilesApi;
using ICTProfilingV3.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ICTProfilingV3.API
{
    public class APIDependencyRegistrar
    {
        public static void RegisterDependencies(IServiceCollection services)
        {
            services.AddSingleton<TokenCache>();
            services.AddSingleton<HTTPNetworkFolder>();
            services.AddTransient<IHTTPNetworkFolder, HTTPNetworkFolder>();
        }
    }
}
