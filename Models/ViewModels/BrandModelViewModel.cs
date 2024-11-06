using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class BrandModelViewModel
    {
        public string Equipment { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int BrandId { get; set; }
        public int ModelId { get; set; }
        public int EquipmentSpecsId { get; set; }
    }
}