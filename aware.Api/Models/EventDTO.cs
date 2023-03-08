using Microsoft.Graph;

namespace aware.Api.Models
{
    public class EventDTO
    {

        public string Id { get; set; }
        public string Subject { get; set; }
        public DateTimeOffset CreatedDateTime { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }


        public EventDTO(string id, string subject, DateTimeOffset? createdDateTime, DateTime start, DateTime end)
        {
            Id = id;
            Subject = subject;
            CreatedDateTime = (DateTimeOffset)createdDateTime;
            StartDate = start;
            EndDate = end;
        }
    }
}
