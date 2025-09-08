using Microsoft.Extensions.DependencyInjection;
using System;

namespace ICTProfilingV3.Mapper.Configurations
{
    public class MapperInitializer
    {
        private readonly IServiceProvider _serviceProvider;
        public MappingConfiguration MapperConfig { get; set; }

        public MapperInitializer(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void InitializeMappings()
        {
            var mapperConfig = _serviceProvider.GetRequiredService<MappingConfiguration>();

            var profiles = _serviceProvider.GetServices<IMappingProfileConfiguration>();

            foreach (var profile in profiles)
            {
                profile.Configure(mapperConfig);
            }
            MapperConfig = mapperConfig;
        }
    }
}
