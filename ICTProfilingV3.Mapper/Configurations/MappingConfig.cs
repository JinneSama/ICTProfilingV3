using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace ICTProfilingV3.Mapper.Configurations
{
    public static class MappingConfig
    {
        public static void RegisterMappings(IServiceCollection services)
        {
            var profiles = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => typeof(IMappingProfileConfiguration).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
                .Select(Activator.CreateInstance)
                .Cast<IMappingProfileConfiguration>();

            foreach (var profile in profiles)
            {
                services.AddSingleton(profile);
            }
            services.AddSingleton<MappingConfiguration>();
        }
    }
}
