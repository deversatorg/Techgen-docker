using ApplicationAuth.Domain.Entities.Identity;
using ApplicationAuth.Helpers.Attributes;
using ApplicationAuth.ResourceLibrary;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Threading.Tasks;

namespace ApplicationAuth.Controllers.API
{
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{api-version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Role.User)]
    [Validate]
    public class VerificationsController : _BaseApiController
    {
        public VerificationsController(IStringLocalizer<ErrorsResource> errorsLocalizer) : base(errorsLocalizer)
        {
        }

        [HttpGet("email")]
        public async Task<IActionResult> Email([FromQuery] string token)
        {
            return View();
        }
    }
}
