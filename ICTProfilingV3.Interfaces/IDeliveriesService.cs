using ICTProfilingV3.DataTransferModels.ViewModels;
using Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ICTProfilingV3.Interfaces
{
    public interface IDeliveriesService : IBaseDataService<Deliveries, int>
    {
        IBaseDataService<DeliveriesSpecs, int> DeliveriesSpecsBaseService { get; set; }
        IBaseDataService<DeliveriesSpecsDetails, long> DeliveriesSpecsDetailsBaseService { get; set; }
        Task<IEnumerable<DeliveriesViewModel>> GetDeliveriesViewModels();
        DeliveriesDetailsViewModel GetDeliveriesDetailViewModels(DeliveriesViewModel model);
    }
}
