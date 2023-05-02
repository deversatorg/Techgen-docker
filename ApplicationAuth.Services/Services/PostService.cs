using ApplicationAuth.Common.Exceptions;
using ApplicationAuth.DAL.Abstract;
using ApplicationAuth.Domain.Entities.PostEntities;
using ApplicationAuth.Models.RequestModels.Post;
using ApplicationAuth.Models.ResponseModels.Base;
using ApplicationAuth.Models.ResponseModels.Port;
using ApplicationAuth.Models.ResponseModels;
using ApplicationAuth.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ApplicationAuth.Common.Extensions;

namespace ApplicationAuth.Services.Services
{
    public class PostService : IPostService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        private int? _userId = null;

        public PostService(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;

            var context = httpContextAccessor.HttpContext;

            if (context?.User != null)
            {
                try
                {
                    _userId = context.User.GetUserId();
                }
                catch
                {
                    _userId = null;
                }
            }
        }

        public async Task<IBaseResponse<MessageResponseModel>> Delete(int id)
        {
            var post = _unitOfWork.Repository<Post>().GetById(id);
            if (post == null)
                throw new CustomException(System.Net.HttpStatusCode.NotFound, "Post not found", "Such post does not exist");

            _unitOfWork.Repository<Post>().DeleteById(id);
            _unitOfWork.SaveChanges();
            return new BaseResponse<MessageResponseModel>() { Data = new MessageResponseModel($"{id} was deleted"), StatusCode = System.Net.HttpStatusCode.OK };
        }

        public async Task<IBaseResponse<PostResponseModel>> Update(int id, PostRequestModel model)
        {
            var post = _unitOfWork.Repository<Post>().GetById(id);

            if (post == null)
                throw new CustomException(System.Net.HttpStatusCode.NotFound, "Post not found", "Such post does not exist");

            if(model.Name != "")
                post.Title = model.Name;
            if(model.Text != "")
                post.Text = model.Text;

            _unitOfWork.Repository<Post>().Update(post);
            _unitOfWork.SaveChanges();
            var response = _mapper.Map<PostResponseModel>(post);

            return new BaseResponse<PostResponseModel>() { Data = response, StatusCode = System.Net.HttpStatusCode.OK };
        }

        public async Task<IBaseResponse<IEnumerable<SmallPostResponseModel>>> GetAll()
        {
            var posts = _unitOfWork.Repository<Post>().GetAll();
            if (posts == null)
                throw new CustomException(System.Net.HttpStatusCode.NotFound, "Posts not found", "Posts are not found");
            var response = _mapper.Map<IEnumerable<SmallPostResponseModel>>(posts);
            return new BaseResponse<IEnumerable<SmallPostResponseModel>>() { Data = response, StatusCode = System.Net.HttpStatusCode.OK };
        }

        public async Task<IBaseResponse<PostResponseModel>> Get(int id)
        {
            var post = _unitOfWork.Repository<Post>().Get(x => x.Id == id)
                                                     .Include(w => w.Likes)
                                                     .Include(w => w.Comments)
                                                     .FirstOrDefault();

            if (post == null)
                throw new CustomException(System.Net.HttpStatusCode.NotFound, "Post not found", "Such post does not exist");

            
            var response = _mapper.Map<PostResponseModel>(post);
            response.LikesCount = post.Likes.Count();
            response.Comments = _mapper.Map<List<CommentResponseModel>>(post.Comments.Where(x => x.ParentCommentId == null));

            return new BaseResponse<PostResponseModel>() { Data = response, StatusCode = System.Net.HttpStatusCode.OK };
        }

        public async Task<IBaseResponse<PostResponseModel>> Create(PostRequestModel model)
        {
            var post = _unitOfWork.Repository<Post>().Get(x => x.Title == model.Name).FirstOrDefault();

            if (post != null)
                throw new CustomException(System.Net.HttpStatusCode.BadRequest, "Title is taken", "Such post-title already exist");

            post = new Post()
            {
                Title = model.Name,
                Text = model.Text,
                UserId = _userId.Value
            };

            _unitOfWork.Repository<Post>().Insert(post);
            _unitOfWork.SaveChanges();

            var response = _mapper.Map<PostResponseModel>(post);
            return new BaseResponse<PostResponseModel>() { Data = response, StatusCode = System.Net.HttpStatusCode.OK };
        }
    }
}
