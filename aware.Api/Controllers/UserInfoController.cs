using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using Microsoft.Graph;
using aware.Api.Models;

namespace aware.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    [RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
    public class UserInfoController : ControllerBase
    {
        private readonly GraphServiceClient _graphServiceClient;

        private readonly ILogger<UserInfoController> _logger;

        public UserInfoController(ILogger<UserInfoController> logger, GraphServiceClient graphServiceClient)
        {
            _logger = logger;
            _graphServiceClient = graphServiceClient;
        }

        //[HttpGet(Name = "GetCalendar")]
        //public async Task<String> GetCalendar()
        //{
        //    var me = await _graphServiceClient.Me.Request().GetAsync();
        //    var cal = await _graphServiceClient.Me.Calendar.Request().GetAsync();        
        //    return me.DisplayName;
        //}

        [HttpGet(Name = "GetProfile")]
        public async Task<ProfileDTO> GetProfile()
        {
            var me = await _graphServiceClient.Me.Request().GetAsync();
            ProfileDTO profileDTO = new ProfileDTO(me.DisplayName, me.UserPrincipalName);
            return profileDTO;
        }
    }
}