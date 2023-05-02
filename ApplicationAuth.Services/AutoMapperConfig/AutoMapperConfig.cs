using ApplicationAuth.Common.Extensions;
using ApplicationAuth.Domain.Entities.Identity;
using ApplicationAuth.Domain.Entities.PostEntities;
using ApplicationAuth.Domain.Entities.RoadmapEntities;
using ApplicationAuth.Models.RequestModels;
using ApplicationAuth.Models.ResponseModels;
using ApplicationAuth.Models.ResponseModels.Port;
using ApplicationAuth.Models.ResponseModels.Roadmaps;
using ApplicationAuth.Models.ResponseModels.Session;
using Profile = ApplicationAuth.Domain.Entities.Identity.Profile;

namespace ApplicationAuth.Services.StartApp
{
    public class AutoMapperProfileConfiguration : AutoMapper.Profile
    {
        public AutoMapperProfileConfiguration()
        : this("MyProfile")
        {
        }

        protected AutoMapperProfileConfiguration(string profileName)
        : base(profileName)
        {

            CreateMap<UserDevice, UserDeviceResponseModel>()
                .ForMember(t => t.AddedAt, opt => opt.MapFrom(src => src.AddedAt.ToISO()));

            #region User model

            CreateMap<UserProfileRequestModel, Profile>()
                .ForMember(t => t.Id, opt => opt.Ignore())
                .ForMember(t => t.User, opt => opt.Ignore());

            CreateMap<Profile, UserResponseModel>()
                .ForMember(t => t.Email, opt => opt.MapFrom(x => x.User != null ? x.User.Email : ""))
                .ForMember(t => t.PhoneNumber, opt => opt.MapFrom(x => x.User != null ? x.User.PhoneNumber : ""))
                .ForMember(t => t.IsBlocked, opt => opt.MapFrom(x => x.User != null ? !x.User.IsActive : false));

            CreateMap<ApplicationUser, UserBaseResponseModel>()
               .IncludeAllDerived();

            CreateMap<ApplicationUser, UserResponseModel>()
                .ForMember(x => x.FirstName, opt => opt.MapFrom(x => x.Profile.FirstName))
                .ForMember(x => x.LastName, opt => opt.MapFrom(x => x.Profile.LastName))
                .ForMember(x => x.IsBlocked, opt => opt.MapFrom(x => !x.IsActive))
                .IncludeAllDerived();

            CreateMap<ApplicationUser, UserRoleResponseModel>();

            #endregion

            #region Comment model

            CreateMap<Comment, CommentResponseModel>()
                .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(x => x.Text, opt => opt.MapFrom(x => x.Text))
                .ForMember(x => x.AnswersToComment, opt => opt.MapFrom(x => x.Answers))
                .ForMember(x => x.ParentCommentId, opt => opt.MapFrom(x => x.ParentCommentId))
                .ForMember(x => x.AuthorId, opt => opt.MapFrom(x => x.UserId));

            #endregion

            #region Roadmap model

            CreateMap<Roadmap, RoadmapResponseModel>()
                .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(x => x.Markdown, opt => opt.MapFrom(x => x.Markdown))
                .ForMember(x => x.ImageName, opt => opt.MapFrom(x => x.ImageName));

            CreateMap<Roadmap, SmallRoadmapResponseModel>()
                .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(x => x.Markdown, opt => opt.MapFrom(x => x.Markdown));
            #endregion

            #region Post model

            CreateMap<Post, PostResponseModel>()
                .ForMember(x => x.LikesCount, opt => opt.MapFrom(x => x.Likes.Count))
                .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Title))
                .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(x => x.Text, opt => opt.MapFrom(x => x.Text))
                .ForMember(x => x.Comments, opt => opt.MapFrom(x => x.Comments));

            CreateMap<Post, SmallPostResponseModel>()
                .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Title))
                .ForMember(x => x.LikesCount, opt => opt.MapFrom(x => x.Likes.Count));

            #endregion

        }
    }
}
