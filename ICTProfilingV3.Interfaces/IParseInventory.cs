using ICTProfilingV3.DataTransferModels.Models;
using Models.Entities;
using System.Threading.Tasks;

namespace ICTProfilingV3.Interfaces
{
    public interface IParseInventory
    {
        Task<Device> Parse(string ppeNo, string Specs);
        Task GeneratePPESpecs(Device device, string ppeNo, Model _model);
    }
}
