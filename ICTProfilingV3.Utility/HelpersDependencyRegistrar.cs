using Helpers.Utility;
using ICTProfilingV3.Interfaces;
using ICTProfilingV3.Utility.Controls;
using ICTProfilingV3.Utility.Scanner;
using ICTProfilingV3.Utility.Security;
using Microsoft.Extensions.DependencyInjection;

namespace ICTProfilingV3.Utility
{
    public static class HelpersDependencyRegistrar
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton<IUCManager,UCManager>();
            services.AddSingleton(typeof(IControlNavigator<>), typeof(ControlNavigator<>));
            services.AddTransient(typeof(IControlMapper<>), typeof(ControlMapper<>));
            services.AddTransient<IEncryptFile, EncryptFile>();
            services.AddTransient<IParseInventory, ParseInventory>();
            services.AddSingleton<ICryptography, Cryptography>();
            services.AddSingleton<IScanDocument, ScanDocument>();
        }
    }
}
