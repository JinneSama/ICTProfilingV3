using Models.Entities;
using System.Linq;

namespace ICTProfilingV3.Interfaces
{
    public interface IEquipmentService
    {
        IQueryable<Equipment> GetAllEquipment();
    }
}
