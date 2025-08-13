using System;

namespace ICTProfilingV3.DataTransferModels
{
    public class CASDetailDTM
    {
        public int Id { get; set; }
        public DateTime? DateCreated { get; set; }
        public string ClientName { get; set; }
        //public long? Employee { get; set; }
        public string Office { get; set; }
        public int Gender { get; set; }
        public string ClientRequest { get; set; }
        public string ContactNo { get; set; }
        public string ActionTaken { get; set; }
        public string AssistedBy { get; set; }
        public string AssistedByName { get; set; }
    }
}
