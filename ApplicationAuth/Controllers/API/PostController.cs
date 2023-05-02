using ApplicationAuth.Common.Constants;
using ApplicationAuth.Models.ResponseModels.Base;
using ApplicationAuth.Models.ResponseModels.Port;
using ApplicationAuth.Models.ResponseModels;
using ApplicationAuth.ResourceLibrary;
using ApplicationAuth.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationAuth.Common.Attributes;

namespace ApplicationAuth.Controllers.API
{
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{api-version:apiVersion}/[controller]")]
    public class PostController : _BaseApiController
    {
        private readonly IPostService _postService;

        public PostController(IStringLocalizer<ErrorsResource> errorsLocalizer,
                              IPostService postService) : base(errorsLocalizer)
        {
            _postService = postService;
        }

        // GET api/v1/post/
        /// <summary>
        /// Get post
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET api/v1/post/123
        ///
        /// </remarks>
        /// <param name="id">post id</param>
        /// <returns>HTTP 200 with PostResponseModel or HTTP 4XX, 500 with error message</returns>
        [SwaggerResponse(200, ResponseMessages.RequestSuccessful, typeof(JsonResponse<IBaseResponse<PostResponseModel>>))]
        [SwaggerResponse(403, ResponseMessages.Forbidden, typeof(ErrorResponseModel))]
        [SwaggerResponse(404, ResponseMessages.NotFound, typeof(ErrorResponseModel))]
        [SwaggerResponse(500, ResponseMessages.InternalServerError, typeof(ErrorResponseModel))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPost([FromRoute][ValidateId]int id)
        {
            var response = await _postService.Get(id);
            return Json(new JsonResponse<IBaseResponse<PostResponseModel>>(response));
        }

        // GET api/v1/post/getall
        /// <summary>
        /// Get all posts
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET api/v1/post/getall
        ///
        /// </remarks>
        /// <returns>HTTP 200 with Array of SmallPostResponseModel or HTTP 4XX, 500 with error message</returns>
        [SwaggerResponse(200, ResponseMessages.RequestSuccessful, typeof(JsonResponse<IBaseResponse<IEnumerable<SmallPostResponseModel>>>))]
        [SwaggerResponse(403, ResponseMessages.Forbidden, typeof(ErrorResponseModel))]
        [SwaggerResponse(404, ResponseMessages.NotFound, typeof(ErrorResponseModel))]
        [SwaggerResponse(500, ResponseMessages.InternalServerError, typeof(ErrorResponseModel))]
        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _postService.GetAll();
            return Json(new JsonResponse<IBaseResponse<IEnumerable<SmallPostResponseModel>>>(response));

        }

    }
}
