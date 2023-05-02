using ApplicationAuth.Models.RequestModels.Post;
using ApplicationAuth.Models.ResponseModels.Base;
using ApplicationAuth.Models.ResponseModels.Port;
using ApplicationAuth.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationAuth.Services.Interfaces
{
    public interface ICommentService
    {
        Task<IBaseResponse<CommentResponseModel>> Create(CommentRequestModel model);
        Task<IBaseResponse<IEnumerable<CommentResponseModel>>> GetAll(int postId);
        Task<IBaseResponse<CommentResponseModel>> CreateAnswer(CommentRequestModel model, int parentId);
        Task<IBaseResponse<MessageResponseModel>> UserDelete(int id);
        Task<IBaseResponse<MessageResponseModel>> AdminDelete(AdminDeleteCommentRequestModel model);

    }
}
