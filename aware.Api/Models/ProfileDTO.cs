namespace aware.Api.Models
{
    public class ProfileDTO
    {
        public string Displayname { get; set; }
        public string userPrincipalName { get; set; }

        public ProfileDTO(string displayName, string uPname)
        {
            Displayname = displayName;
            userPrincipalName = uPname;
        }

    }
}
