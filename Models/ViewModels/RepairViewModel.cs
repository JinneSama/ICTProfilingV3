﻿using Models.Enums;
using System;

namespace Models.ViewModels
{
    public class RepairViewModel
    {
        public int Id { get; set; }
        public TicketStatus Status { get; set; }
        public DateTime? DateCreated { get; set; }
        public string PropertyNo { get; set; }
        public string IssuedTo { get; set; }
        public string Office { get; set; }
        public string RepairId { get; set; }
    }
}
