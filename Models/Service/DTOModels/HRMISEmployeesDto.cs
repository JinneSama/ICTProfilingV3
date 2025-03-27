namespace Models.Service.DTOModels
{
    public class HRMISEmployeesDto
    {
        public long? Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string NameExt { get; set; }
        public bool? Detailed { get; set; }
        public string DetailedToOffice { get; set; }
        public string Office { get; set; }
        public string DetailedToDivision { get; set; }
        public string Division { get; set; }
        public string Position { get; set; }
        public string Username { get; set; }
        public bool IsResigned { get; set; }    
    }
}
