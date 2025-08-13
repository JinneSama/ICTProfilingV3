using ICTProfilingV3.Interfaces;
using Models.Entities;
using System.Linq;

namespace ICTProfilingV3.Services
{
    public class EquipmentService : IEquipmentService
    {
        private readonly IRepository<int, Equipment> _equipmentRepository;
        public EquipmentService(IRepository<int, Equipment> equipmentRepository)
        {
            _equipmentRepository = equipmentRepository;
        }
        public IQueryable<Equipment> GetAllEquipment()
        {
            return _equipmentRepository.GetAll();
        }
    }
}
