using ApplicationAuth.Models.RequestModels;
using ApplicationAuth.Models.ResponseModels.Base;
using ApplicationAuth.Models.ResponseModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationAuth.Models.ResponseModels.Roadmaps;

namespace ApplicationAuth.Services.Interfaces
{
    public interface IRoadmapService
    {
        public Task<IBaseResponse<RoadmapResponseModel>> Create(IFormFile image, string markdown);

        public IBaseResponse<RoadmapResponseModel> Get(int id);

        public IBaseResponse<MessageResponseModel> Delete(int id);

        public Task<IBaseResponse<RoadmapResponseModel>> Update(int id, RoadmapRequestModel model);

        public Task<IBaseResponse<IEnumerable<SmallRoadmapResponseModel>>> GetAll();
    }
}
