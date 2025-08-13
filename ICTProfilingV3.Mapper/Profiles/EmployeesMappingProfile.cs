using ICTProfilingV3.DataTransferModels;
using ICTProfilingV3.DataTransferModels.ServiceModels.DTOModels;
using ICTProfilingV3.DataTransferModels.ViewModels;
using ICTProfilingV3.Mapper.Configurations;

namespace ICTProfilingV3.Mapper.Profiles
{
    public class EmployeesMappingProfile : IMappingProfileConfiguration
    {
        public void Configure(MappingConfiguration config)
        {
            config.AddConfiguration<HRMISEmployeesDto, EmployeesViewModel>(cfg =>
            {
                cfg.CreateMap(u => u.Id, vm => vm.Id);
                cfg.CreateMap(u => $"{u.FirstName} {u.MiddleName} {u.LastName} {u.NameExt}", vm => vm.Employee);
                cfg.CreateMap(
                    u => u.Detailed ?? false ? u.DetailedToOffice : u.Office, vm => vm.Office
                );
                cfg.CreateMap(
                    u => u.Detailed ?? false ? u.DetailedToDivision : u.Division, vm => vm.Division
                );
                cfg.CreateMap(u => u.Position, vm => vm.Position);
                cfg.CreateMap(u => u.Username, vm => vm.Username);
                cfg.CreateMap(u => u.FirstName, vm => vm.FirstName);
                cfg.CreateMap(u => u.LastName, vm => vm.LastName);
                cfg.CreateMap(u => u.IsResigned, vm => vm.IsResigned);
            });

            config.AddConfiguration<OFMISEmployeesDto, EmployeesViewModel>(cfg =>
            {
                cfg.CreateMap(u => u.Id, vm => vm.Id);
                cfg.CreateMap(u => $"{u.FirstName} {u.MiddleName} {u.LastName} {u.ExtName}", vm => vm.Employee);
                cfg.CreateMap(u => u.Office, vm => vm.Office);
                cfg.CreateMap(u => u.Position, vm => vm.Position);
                cfg.CreateMap(u => u.FirstName, vm => vm.FirstName);
                cfg.CreateMap(u => u.LastName, vm => vm.LastName);
                cfg.CreateMap(u => u.ChiefId, vm => vm.ChiefId);
            });
        }
    }
}
