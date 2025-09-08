using System;
using System.Drawing;

namespace ICTProfilingV3.DataTransferModels.ViewModels
{
    public class SampleViewModel
    {
        public DateTime? DateCreated { get; set; }
        public string Version { get; set; }
        public string Changes { get; set; }
        public Image Image { get; set; }
    }
}
