
using Mapper;
using Models.Service.DTOModels;
using Models.ViewModels;

namespace Models.MappingConfigurations
{
    public static class MappingConfig
    {
        private static MappingConfiguration _MapperConfig;
        public static MappingConfiguration MapperConfig
        {
            get
            {
                return _MapperConfig ?? (_MapperConfig = ConfigureMappings());
            }
        }

        public static MappingConfiguration ConfigureMappings()
        {
            var mapperConfig = new MappingConfiguration();

            mapperConfig.AddConfiguration<HRMISEmployeesDto, EmployeesViewModel>(cfg =>
            {
                cfg.CreateMap(u => u.Id, vm => vm.Id);
                cfg.CreateMap(u => $"{u.FirstName} {u.MiddleName} {u.LastName} {u.NameExt}", vm => vm.Employee);
                cfg.CreateMap(
                    u => u.Detailed ?? false ? u.DetailedToOffice : u.Office,vm => vm.Office
                );
                cfg.CreateMap(
                    u => u.Detailed ?? false ? u.DetailedToDivision : u.Division,vm => vm.Division
                );
                cfg.CreateMap(u => u.Position, vm => vm.Position);
                cfg.CreateMap(u => u.Username, vm => vm.Username);
                cfg.CreateMap(u => u.FirstName, vm => vm.FirstName);
                cfg.CreateMap(u => u.LastName, vm => vm.LastName);
            });

            mapperConfig.AddConfiguration<OFMISEmployeesDto, EmployeesViewModel>(cfg =>
            {
                cfg.CreateMap(u => u.Id, vm => vm.Id);
                cfg.CreateMap(u => $"{u.FirstName} {u.MiddleName} {u.LastName} {u.ExtName}", vm => vm.Employee);
                cfg.CreateMap(u => u.Office, vm => vm.Office);
                cfg.CreateMap(u => u.Position, vm => vm.Position);
                cfg.CreateMap(u => u.FirstName, vm => vm.FirstName);
                cfg.CreateMap(u => u.LastName, vm => vm.LastName);
                cfg.CreateMap(u => u.ChiefId, vm => vm.ChiefId);
            });

            return mapperConfig;
        }
    }
}
