using System.Collections.Generic;

namespace ICTProfilingV3.DataTransferModels.Models
{
    public class Device
    {
        public string DeviceType { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public List<Specs> Specs { get; set; } = new List<Specs>();
    }

    public class Specs
    {
        public int ItemNo { get; set; }
        public string SpecsName { get; set; }
        public string Description { get; set; }
    }
}
