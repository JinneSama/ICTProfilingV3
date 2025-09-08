using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICTProfilingV3.DataTransferModels.ViewModels
{
    public class BrandModelViewModel
    {
        public int? EquipmentCategoryBrandId { get; set; }
        public string Equipment { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string EquipmentBrand { get; set; }
        public int? EquipmentBrandId { get; set; }
        public int BrandId { get; set; }
        public int ModelId { get; set; }
        public int EquipmentSpecsId { get; set; }
    }
}