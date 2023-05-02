using ApplicationAuth.Domain.Entities.Identity;
using ApplicationAuth.Models.RequestModels;
using ApplicationAuth.Models.ResponseModels.Base;
using ApplicationAuth.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationAuth.Services.Interfaces
{
    public interface IProfileService
    {
        Task<IBaseResponse<UserResponseModel>> Edit(ProfileRequestModel model);
        Task<IBaseResponse<UserResponseModel>> Create(ApplicationUser user);
        Task<IBaseResponse<UserResponseModel>> Get();
    }
}
