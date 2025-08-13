using Models.Enums;

namespace ICTProfilingV3.DataTransferModels.Models
{
    public class EvaluationRating
    {
        public string Staff { get; set; }
        public int Requested { get; set; }
        public int Items { get; set; }
        public decimal Rating { get; set; }
        public int Quantity { get; set; }
        public int Male { get; set; }
        public int Female { get; set; }
        public Gender Gender { get; set; }
    }
}
