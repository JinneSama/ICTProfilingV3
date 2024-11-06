using Models.Enums;
using System;

namespace Models.ViewModels
{
    public class PPEsViewModel
    {
        public int Id { get; set; }
        public string PropertyNo { get; set; }
        public string IssuedTo { get; set; }
        public string Office { get; set; }
        public DateTime? DateCreated { get; set; }
        public PPEStatus? Status { get; set; }
    }
}
