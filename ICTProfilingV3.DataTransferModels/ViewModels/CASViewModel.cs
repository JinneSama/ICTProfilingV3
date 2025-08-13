using Models.Entities;
using System;

namespace ICTProfilingV3.DataTransferModels.ViewModels
{
    public class CASViewModel
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public string Office { get; set; }
        public string Request { get; set; }
        public string AssistedBy { get; set; }
        public CustomerActionSheet CustomerActionSheet { get; set; }
    }
}
