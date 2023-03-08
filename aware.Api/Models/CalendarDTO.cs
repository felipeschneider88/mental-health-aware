namespace aware.Api.Models
{
    public class CalendarDTO
    {
        public string OwnerName { get; set; }
        public string OwnerEmail { get; set; }
        public string Name { get; set; }

        public CalendarDTO(string pName, string pOwnerName, String pOwnerEmail)
        {
            OwnerName = pName;
            OwnerEmail = pOwnerEmail;
            Name = pName;
        }

    }
}
