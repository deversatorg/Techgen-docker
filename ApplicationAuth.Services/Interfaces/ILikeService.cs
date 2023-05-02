﻿using ApplicationAuth.Models.ResponseModels.Base;
using ApplicationAuth.Models.ResponseModels.Port;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationAuth.Services.Interfaces
{
    public interface ILikeService
    {
        Task<IBaseResponse<PostResponseModel>> Create(int postId);
        Task<IBaseResponse<PostResponseModel>> Delete(int postId);
    }
}
