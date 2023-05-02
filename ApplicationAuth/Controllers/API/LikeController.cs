using ApplicationAuth.Common.Constants;
using ApplicationAuth.Models.ResponseModels.Base;
using ApplicationAuth.Models.ResponseModels.Port;
using ApplicationAuth.Models.ResponseModels;
using ApplicationAuth.ResourceLibrary;
using ApplicationAuth.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using ApplicationAuth.Common.Attributes;

namespace ApplicationAuth.Controllers.API
{
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{api-version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class LikeController : _BaseApiController
    {
        private readonly ILikeService _likeSwrvice;

        public LikeController(IStringLocalizer<ErrorsResource> errorsLocalizer, ILikeService likeService) : base(errorsLocalizer)
        {
            _likeSwrvice = likeService;
        }

        // GET api/v1/like/{postId}
        /// <summary>
        /// Like post
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET api/v1/like/123
        ///
        /// </remarks>
        /// <param name="id">post id</param>
        /// <returns>HTTP 200 with post response model or HTTP 4XX, 500 with error message</returns>
        [SwaggerResponse(200, ResponseMessages.RequestSuccessful, typeof(JsonResponse<IBaseResponse<PostResponseModel>>))]
        [SwaggerResponse(401, ResponseMessages.Unauthorized, typeof(ErrorResponseModel))]
        [SwaggerResponse(403, ResponseMessages.Forbidden, typeof(ErrorResponseModel))]
        [SwaggerResponse(404, ResponseMessages.NotFound, typeof(ErrorResponseModel))]
        [SwaggerResponse(500, ResponseMessages.InternalServerError, typeof(ErrorResponseModel))]
        [HttpGet("{id}")]
        public async Task<IActionResult> Create([FromRoute][Required(ErrorMessage = "post id is required")][ValidateId]int id)
        {
            var response = await _likeSwrvice.Create(id);
            return Json(new JsonResponse<IBaseResponse<PostResponseModel>>(response));
        }

    }
}
