namespace Models.Service.DTOModels
{
    public class PRDetailsDto
    {
        public int? ItemNo { get; set; }
        public decimal? Quantity { get; set; }
        public string UOM { get; set; }
        public string Item { get; set; }
        public string Category { get; set; }
        public decimal? Cost { get; set; }
        public decimal? TotalCost { get; set; }
    }
}
