namespace ICTProfilingV3.DataTransferModels.ServiceModels.DTOModels
{
    public class OFMISEmployeesDto
    {
        public long? Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string ExtName { get; set; }
        public string Position { get; set; }
        public string Office { get; set; }
        public long? ChiefId { get; set; }
    }
}
