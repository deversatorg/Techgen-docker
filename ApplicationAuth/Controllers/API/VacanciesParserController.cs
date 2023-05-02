using ApplicationAuth.Common.Constants;
using ApplicationAuth.Helpers.Attributes;
using ApplicationAuth.Models.RequestModels;
using ApplicationAuth.Models.ResponseModels.Base;
using ApplicationAuth.Models.ResponseModels;
using ApplicationAuth.ResourceLibrary;
using ApplicationAuth.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationAuth.Controllers.API
{
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{api-version:apiVersion}/vacansies")]
    public class VacanciesParserController : _BaseApiController
    {
        private readonly IVacanciesParser _vacanciesParser;

        public VacanciesParserController(IStringLocalizer<ErrorsResource> errorsLocalizer, IVacanciesParser vacanciesParser) : base(errorsLocalizer)
        {
            _vacanciesParser = vacanciesParser;
        }

        // GET api/v1/vacansies/
        /// <summary>
        /// Vacansies from sites
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     GET api/v1/vacansies/?SearchQuery=string&amp;Employment=string&amp;Direction=string&amp;
        ///     
        /// </remarks>
        /// <param name="model">Pagination request model</param>
        /// <returns>A vacansies list in pagination</returns>
        [HttpGet]
        [PreventSpam(Name = "Vacansies")]
        [SwaggerResponse(200, ResponseMessages.RequestSuccessful, typeof(JsonResponse<IBaseResponse<IEnumerable<VacancyResponseModel>>>))]
        [SwaggerResponse(403, ResponseMessages.Forbidden, typeof(ErrorResponseModel))]
        [SwaggerResponse(404, ResponseMessages.NotFound, typeof(ErrorResponseModel))]
        [SwaggerResponse(500, ResponseMessages.InternalServerError, typeof(ErrorResponseModel))]
        public async Task<IActionResult> Vacansies([FromQuery] VacancyRequestModel model)
        {
            var response = await _vacanciesParser.GetAll(model);
            return Json(new JsonResponse<IBaseResponse<List<VacancyResponseModel>>>(response));
        }
    }
}
