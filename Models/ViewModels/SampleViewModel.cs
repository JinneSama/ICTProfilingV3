using System;
using System.Drawing;

namespace Models.ViewModels
{
    public class SampleViewModel
    {
        public DateTime? DateCreated { get; set; }
        public string Version { get; set; }
        public string Changes { get; set; }
        public Image Image { get; set; }
    }
}
