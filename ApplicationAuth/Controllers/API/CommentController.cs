using ApplicationAuth.Common.Constants;
using ApplicationAuth.Models.RequestModels.Post;
using ApplicationAuth.Models.ResponseModels.Base;
using ApplicationAuth.Models.ResponseModels.Port;
using ApplicationAuth.Models.ResponseModels;
using ApplicationAuth.ResourceLibrary;
using ApplicationAuth.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;
using ApplicationAuth.Domain.Entities.Identity;
using System.ComponentModel.DataAnnotations;
using ApplicationAuth.Common.Attributes;

namespace ApplicationAuth.Controllers.API
{
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{api-version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CommentController : _BaseApiController
    {
        private readonly ICommentService _commentService;
        public CommentController(IStringLocalizer<ErrorsResource> errorsLocalizer, ICommentService commentService) : base(errorsLocalizer)
        {
            _commentService = commentService;
        }

        // POST api/v1/comment/
        /// <summary>
        /// Create a comment
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST api/v1/comment/
        ///     {                
        ///         "Text" : "sample comment",
        ///         "PostId" : "123",
        ///     }
        ///
        /// </remarks>
        /// <returns>HTTP 200 with comment response model or HTTP 4XX, 500 with error message</returns>
        [SwaggerResponse(200, ResponseMessages.RequestSuccessful, typeof(JsonResponse<IBaseResponse<CommentResponseModel>>))]
        [SwaggerResponse(401, ResponseMessages.Unauthorized, typeof(ErrorResponseModel))]
        [SwaggerResponse(403, ResponseMessages.Forbidden, typeof(ErrorResponseModel))]
        [SwaggerResponse(404, ResponseMessages.NotFound, typeof(ErrorResponseModel))]
        [SwaggerResponse(500, ResponseMessages.InternalServerError, typeof(ErrorResponseModel))]
        [HttpPost("")]
        public async Task<IActionResult> Comment([FromBody] CommentRequestModel model)
        {
            var response = await _commentService.Create(model);
            return Json(new JsonResponse<IBaseResponse<CommentResponseModel>>(response));
        }

        // POST api/v1/comment/{id}
        /// <summary>
        /// Create an answer for a comment
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST api/v1/comment/123
        ///     {                
        ///         "Text" : "sample answer",
        ///         "PostId" : "123",
        ///     }
        ///
        /// </remarks>
        /// <returns>HTTP 200 with comment response model or HTTP 4XX, 500 with error message</returns>
        [SwaggerResponse(200, ResponseMessages.RequestSuccessful, typeof(JsonResponse<IBaseResponse<CommentResponseModel>>))]
        [SwaggerResponse(401, ResponseMessages.Unauthorized, typeof(ErrorResponseModel))]
        [SwaggerResponse(403, ResponseMessages.Forbidden, typeof(ErrorResponseModel))]
        [SwaggerResponse(404, ResponseMessages.NotFound, typeof(ErrorResponseModel))]
        [SwaggerResponse(500, ResponseMessages.InternalServerError, typeof(ErrorResponseModel))]
        [HttpPost("{id}")]
        public async Task<IActionResult> Answer([FromRoute][Required(ErrorMessage = "Parent commment id is required")] int id, [FromBody] CommentRequestModel model)
        {
            var response = await _commentService.CreateAnswer(model, id);
            return Json(new JsonResponse<IBaseResponse<CommentResponseModel>>(response));
        }

        // DELETE api/v1/comment/{id}
        /// <summary>
        /// User delete his/her comment
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE api/v1/comment/123
        ///
        /// </remarks>
        /// <returns>HTTP 200 with comment response model or HTTP 4XX, 500 with error message</returns>
        [SwaggerResponse(200, ResponseMessages.RequestSuccessful, typeof(JsonResponse<IBaseResponse<CommentResponseModel>>))]
        [SwaggerResponse(400, ResponseMessages.NotFound, typeof(ErrorResponseModel))]
        [SwaggerResponse(401, ResponseMessages.Unauthorized, typeof(ErrorResponseModel))]
        [SwaggerResponse(403, ResponseMessages.Forbidden, typeof(ErrorResponseModel))]
        [SwaggerResponse(500, ResponseMessages.InternalServerError, typeof(ErrorResponseModel))]
        [HttpDelete("{id}")]
        public async Task<IActionResult> UserDeleteComment([FromRoute][Required(ErrorMessage = "Commment id is required")][ValidateId] int id)
        {
            var response = await _commentService.UserDelete(id);
            return Json(new JsonResponse<IBaseResponse<MessageResponseModel>>(response));
        }

        // DELETE api/v1/comment/
        /// <summary>
        /// Admin delete a comment
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE api/v1/comment/
        ///
        /// </remarks>
        /// <returns>HTTP 200 with message of successfully delete or HTTP 4XX, 500 with error message</returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Role.Admin)]
        [SwaggerResponse(200, ResponseMessages.RequestSuccessful, typeof(JsonResponse<IBaseResponse<CommentResponseModel>>))]
        [SwaggerResponse(401, ResponseMessages.Unauthorized, typeof(ErrorResponseModel))]
        [SwaggerResponse(403, ResponseMessages.Forbidden, typeof(ErrorResponseModel))]
        [SwaggerResponse(404, ResponseMessages.NotFound, typeof(ErrorResponseModel))]
        [SwaggerResponse(500, ResponseMessages.InternalServerError, typeof(ErrorResponseModel))]
        [HttpDelete("")]
        public async Task<IActionResult> AdminDeleteComment([FromBody] AdminDeleteCommentRequestModel model)
        {
            var response = await _commentService.AdminDelete(model);
            return Json(new JsonResponse<IBaseResponse<MessageResponseModel>>(response));
        }


    }
}
