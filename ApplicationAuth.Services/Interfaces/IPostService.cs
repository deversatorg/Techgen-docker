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
    public interface IPostService
    {
        Task<IBaseResponse<PostResponseModel>> Create(PostRequestModel model);
        Task<IBaseResponse<PostResponseModel>> Update(int id, PostRequestModel model);
        Task<IBaseResponse<PostResponseModel>> Get(int id);
        Task<IBaseResponse<IEnumerable<SmallPostResponseModel>>> GetAll();
        Task<IBaseResponse<MessageResponseModel>> Delete(int id);
    }
}
