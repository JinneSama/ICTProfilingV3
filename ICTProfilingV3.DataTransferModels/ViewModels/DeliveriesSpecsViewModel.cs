using Models.Entities;
using Models.Enums;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace ICTProfilingV3.DataTransferModels.ViewModels
{
    public class DeliveriesSpecsViewModel
    {
        public int Id    { get; set; }
        public int ItemNo { get; set; }
        public int Quantity { get; set; }
        public Unit Unit { get; set; }
        public string Equipment { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public long UnitCost { get; set; }
        public long TotalCost { get; set; }
        public IEnumerable<DeliveriesSpecsDetails> DeliveriesSpecsDetails { get; set; }
        public BindingList<DeliveriesSpecsDetails> Specifications 
        {
            get {
                return new BindingList<DeliveriesSpecsDetails>(DeliveriesSpecsDetails.ToList());
            }
        }
    }
}
