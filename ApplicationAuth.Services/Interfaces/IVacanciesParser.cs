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
    public interface IVacanciesParser
    {
        public Task<BaseResponse<List<VacancyResponseModel>>> GetAll(VacancyRequestModel model);
    }
}
