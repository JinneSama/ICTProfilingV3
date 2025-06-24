namespace ICTProfilingV3.DataTransferModels
{
    public class EmployeesDTM
    {
        public long? Id { get; set; }
        public string Employee { get; set; }
        public string Username { get; set; }
        public string Position { get; set; }
        public string Office { get; set; }
        public string Division { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long? ChiefId { get; set; }
        public bool IsResigned { get; set; }
    }
}
