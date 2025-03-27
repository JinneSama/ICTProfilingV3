using Helpers.Inventory;
using Models.Entities;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace Helpers.Interfaces
{
    public interface IParseInventory
    {
        Task<Device> Parse(string ppeNo, string Specs);
        Task GeneratePPESpecs(Device device, string ppeNo, Model _model);
    }
}
