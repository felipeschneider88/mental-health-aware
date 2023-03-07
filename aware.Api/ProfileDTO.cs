namespace aware.Api
{
    public class ProfileDTO
    {
        public String Displayname { get; set; }
        public String userPrincipalName { get; set; }

        public ProfileDTO(string displayName, String uPname) 
        { 
            Displayname= displayName;
            userPrincipalName= uPname;
        }

    }
}
