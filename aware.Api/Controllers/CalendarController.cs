using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using Microsoft.Graph;
using aware.Api.Models;
using Microsoft.Extensions.Azure;

namespace aware.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    [RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
    public class CalendarController : ControllerBase
    {
        private readonly GraphServiceClient _graphServiceClient;

        private readonly ILogger<CalendarController> _logger;

        public CalendarController(ILogger<CalendarController> logger, GraphServiceClient graphServiceClient)
        {
            _logger = logger;
            _graphServiceClient = graphServiceClient;
        }

        [HttpGet]
        public async Task<CalendarDTO> GetCalendar()
        {
            var cal = await _graphServiceClient.Me.Calendar.Request().GetAsync();
            CalendarDTO result = new CalendarDTO(cal.Name, cal.Owner.Name, cal.Owner.Address);
            return result;
        }

        [HttpGet]
        [Route("events")]
        public async Task<IEnumerable<EventDTO>> getTodayEvents()
        {
            var startTime = DateTime.Today.ToString("s");
            var endTime = DateTime.Now.AddDays(7).ToString("s");
            var queryOptions = new List<QueryOption>()
            {
                new QueryOption("startDateTime", startTime),
                new QueryOption("endDateTime", endTime)
            };
            var aux = await _graphServiceClient.Me.CalendarView.Request(queryOptions).GetAsync();
            var result = new List<EventDTO>();
            foreach (var item in aux)
            {
                EventDTO temp = new EventDTO(item.Id, item.Subject, item.CreatedDateTime, DateTime.Parse(item.Start.DateTime), DateTime.Parse(item.End.DateTime));
                result.Add(temp);
            }
            return result;
        }

        [HttpPost]
        [Route("20minevent")]
        public async Task<Event> PostNewShortMeeting()
        {
            string start = DateTimeOffset.UtcNow.ToString();
            string end = DateTimeOffset.UtcNow.AddMinutes(20).ToString();
            var requestBody = new Event
            {
                Subject = "taking a 20min break",
                Body = new ItemBody()
                {
                    Content = "Go and take a break, grab a cup of coffee, take a walk or stretch your legs, you deserve it, you earn it"
                },
                Start = new DateTimeTimeZone
                {
                    DateTime = start ,
                    TimeZone = "UTC",
                },
                End = new DateTimeTimeZone
                {
                    DateTime = end,
                    TimeZone = "UTC",
                },
            };
            var result = await _graphServiceClient.Me.Events.Request().AddAsync(requestBody);
            return result;
        }

        [HttpPost]
        [Route("WholeDayEvent")]
        public async Task<Event> PostWholeDayMeeting()
        {
            string start = DateTimeOffset.UtcNow.ToString("yyyy-MM-ddT00:00:00");
            string end = DateTimeOffset.UtcNow.AddDays(1).ToString("yyyy-MM-ddT00:00:00");
            var requestBody = new Event
            {
                IsAllDay = true,
                Subject = "OOO - Mental Health day off",
                Body = new ItemBody()
                {
                    Content = "Blocking the day to take a day off so I can work on my mental health and get better"
                },
                Start = new DateTimeTimeZone
                {
                    DateTime = start,
                    TimeZone = "UTC",
                },
                End = new DateTimeTimeZone
                {
                    DateTime = end,
                    TimeZone = "UTC",
                },
            };
            var result = await _graphServiceClient.Me.Events.Request().AddAsync(requestBody);
            return result;
        }
    }
}