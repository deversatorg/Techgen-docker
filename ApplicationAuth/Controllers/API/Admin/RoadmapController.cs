using ApplicationAuth.Common.Constants;
using ApplicationAuth.Models.RequestModels;
using ApplicationAuth.Models.ResponseModels.Base;
using ApplicationAuth.Models.ResponseModels;
using ApplicationAuth.ResourceLibrary;
using ApplicationAuth.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;
using ApplicationAuth.Domain.Entities.Identity;
using iText.Layout.Element;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using ApplicationAuth.Models.ResponseModels.Roadmaps;
using ApplicationAuth.Helpers.Attributes;

namespace ApplicationAuth.Controllers.API.Admin
{
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{api-version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Role.Admin)]
    public class RoadmapController : _BaseApiController
    {
        private readonly IRoadmapService _roadmapService;
        public RoadmapController(IStringLocalizer<ErrorsResource> errorsLocalizer, IRoadmapService roadmapService) : base(errorsLocalizer)
        {
            _roadmapService = roadmapService;
        }

        // POST api/v1/roadmap/
        /// <summary>
        /// Create a roadmap
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     POST api/v1/roadmap/
        ///     -H "Content-Type: multipart/form-data" \
        ///     -F "Image=@/path/to/image.jpg" \
        ///     -F "Markdown=# Heading 1\n\nSome text here"
        ///     
        /// </remarks>
        /// <param name="image">image file of roadmap</param>
        /// <param name="markdown">string which will be markdown parsed</param>
        /// <returns>HTTP 200 with RoadmapResponseModel which include base64 image or HTTP 4XX, 500 with error message</returns>
        [SwaggerResponse(200, ResponseMessages.RequestSuccessful, typeof(JsonResponse<IBaseResponse<RoadmapResponseModel>>))]
        [SwaggerResponse(400, ResponseMessages.BadRequest, typeof(ErrorResponseModel))]
        [SwaggerResponse(401, ResponseMessages.Unauthorized, typeof(ErrorResponseModel))]
        [SwaggerResponse(403, ResponseMessages.Forbidden, typeof(ErrorResponseModel))]
        [SwaggerResponse(500, ResponseMessages.InternalServerError, typeof(ErrorResponseModel))]
        [HttpPost("")]
        [PreventSpam(Name = "Create")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create(IFormFile image, [FromForm] string markdown)
        {
            var response = await _roadmapService.Create(image, markdown);

            return Json(new JsonResponse<IBaseResponse<RoadmapResponseModel>>(response));
        }

        [SwaggerResponse(200, ResponseMessages.SuccessfulRegistration, typeof(JsonResponse<IBaseResponse<RoadmapResponseModel>>))]
        [SwaggerResponse(400, ResponseMessages.BadRequest, typeof(ErrorResponseModel))]
        [SwaggerResponse(403, ResponseMessages.Forbidden, typeof(ErrorResponseModel))]
        [SwaggerResponse(500, ResponseMessages.InternalServerError, typeof(ErrorResponseModel))]
        [HttpPost("test")]
        public async Task<IActionResult> TestCreat(IFormFile image, string markdown)
        {
            var response = await _roadmapService.Create(image, markdown);

            return Json(new JsonResponse<IBaseResponse<RoadmapResponseModel>>(response));
        }

        // GET api/v1/roadmap/{id}
        /// <summary>
        /// Get a roadmap by Id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     GET api/v1/roadmap/123
        ///     
        /// </remarks>
        /// <param name="id">Roadmap id</param>
        /// <returns>HTTP 200 with RoadmapResponseModel which include base64 image or HTTP 4XX, 500 with error message</returns>
        [SwaggerResponse(200, ResponseMessages.RequestSuccessful, typeof(JsonResponse<IBaseResponse<RoadmapResponseModel>>))]
        [SwaggerResponse(404, ResponseMessages.NotFound, typeof(ErrorResponseModel))]
        [SwaggerResponse(403, ResponseMessages.Forbidden, typeof(ErrorResponseModel))]
        [SwaggerResponse(500, ResponseMessages.InternalServerError, typeof(ErrorResponseModel))]
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = _roadmapService.Get(id);

            return Json(new JsonResponse<IBaseResponse<RoadmapResponseModel>>(response));
        }

        // GET api/v1/roadmap/
        /// <summary>
        /// Get all roadmaps
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     GET api/v1/roadmap
        ///     
        /// </remarks>
        /// <returns>HTTP 200 with SmallRoadmapResponseModel or HTTP 4XX, 500 with error message</returns>
        [SwaggerResponse(200, ResponseMessages.RequestSuccessful, typeof(JsonResponse<IBaseResponse<List<string>>>))]
        [SwaggerResponse(404, ResponseMessages.NotFound, typeof(ErrorResponseModel))]
        [SwaggerResponse(403, ResponseMessages.Forbidden, typeof(ErrorResponseModel))]
        [SwaggerResponse(500, ResponseMessages.InternalServerError, typeof(ErrorResponseModel))]
        [AllowAnonymous]
        [HttpGet("")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _roadmapService.GetAll();

            return Json(new JsonResponse<IBaseResponse<IEnumerable<SmallRoadmapResponseModel>>>(response));
        }

        // DELETE api/v1/roadmap/{id}
        /// <summary>
        /// Delete a roadmap by Id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     DELETE api/v1/roadmap/123
        ///     
        /// </remarks>
        /// <param name="id">Roadmap id</param>
        /// <returns>HTTP 200 with message of successfully delete or HTTP 4XX, 500 with error message</returns>
        [SwaggerResponse(200, ResponseMessages.SuccessfulRegistration, typeof(JsonResponse<IBaseResponse<MessageResponseModel>>))]
        [SwaggerResponse(401, ResponseMessages.Unauthorized, typeof(ErrorResponseModel))]
        [SwaggerResponse(403, ResponseMessages.Forbidden, typeof(ErrorResponseModel))]
        [SwaggerResponse(404, ResponseMessages.NotFound, typeof(ErrorResponseModel))]
        [SwaggerResponse(500, ResponseMessages.InternalServerError, typeof(ErrorResponseModel))]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = _roadmapService.Delete(id);

            return Json(new JsonResponse<IBaseResponse<MessageResponseModel>>(response));
        }


        // PUT api/v1/roadmap/{id}
        /// <summary>
        /// Update a roadmap
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     POST api/v1/roadmap/123
        ///     -H "Content-Type: multipart/form-data" \
        ///     -F "image=@/path/to/image.jpg" \
        ///     -F "markdown=# Heading 1\n\nSome text here"
        ///     
        /// </remarks>
        /// <param name="id">Mardown Id</param>
        /// <param name="model">Reuqest model with data to update</param>
        /// <returns>HTTP 200 with RoadmapResponseModel or HTTP 4XX, 500 with error message</returns>
        [SwaggerResponse(200, ResponseMessages.RequestSuccessful, typeof(JsonResponse<IBaseResponse<RoadmapResponseModel>>))]
        [SwaggerResponse(400, ResponseMessages.BadRequest, typeof(ErrorResponseModel))]
        [SwaggerResponse(401, ResponseMessages.Unauthorized, typeof(ErrorResponseModel))]
        [SwaggerResponse(403, ResponseMessages.Forbidden, typeof(ErrorResponseModel))]
        [SwaggerResponse(404, ResponseMessages.NotFound, typeof(ErrorResponseModel))]
        [SwaggerResponse(500, ResponseMessages.InternalServerError, typeof(ErrorResponseModel))]
        [HttpPut("{id}")]
        [PreventSpam(Name = "Update")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Update(int id, [FromForm] RoadmapRequestModel model)
        {
            var response = await _roadmapService.Update(id, model);

            return Json(new JsonResponse<IBaseResponse<RoadmapResponseModel>>(response));
        }

    }
}
