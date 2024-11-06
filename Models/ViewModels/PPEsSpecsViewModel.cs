using Models.Entities;
using Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class PPEsSpecsViewModel
    {
        public int Id { get; set; }
        public int ItemNo { get; set; }
        public int Quantity { get; set; }
        public Unit Unit { get; set; }
        public string Equipment { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public long UnitCost { get; set; }
        public long TotalCost { get; set; }
        public bool? Mark { get; set; } = false;
        public IEnumerable<PPEsSpecsDetails> PPEsSpecsDetails { get; set; }
        public BindingList<PPEsSpecsDetails> Specifications
        {
            get
            {
                return new BindingList<PPEsSpecsDetails>(PPEsSpecsDetails.ToList());
            }
        }
    }
}
