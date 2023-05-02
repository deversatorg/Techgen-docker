using ApplicationAuth.Common.Exceptions;
using ApplicationAuth.DAL.Abstract;
using ApplicationAuth.Domain.Entities.RoadmapEntities;
using ApplicationAuth.Models.RequestModels;
using ApplicationAuth.Models.ResponseModels.Base;
using ApplicationAuth.Models.ResponseModels;
using ApplicationAuth.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationAuth.Common.Utilities;
using ApplicationAuth.Domain.Entities.PostEntities;
using ApplicationAuth.Models.ResponseModels.Roadmaps;
using ApplicationAuth.Common.Extensions;

namespace ApplicationAuth.Services.Services
{
    public class RoadmapService : IRoadmapService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _appEnvironment;

        public RoadmapService(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment appEnvironment)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _appEnvironment = appEnvironment;
        }

        public async Task<IBaseResponse<RoadmapResponseModel>> Create(IFormFile image, string markdown)
        {


            if (image == null || markdown == null)
            {
                throw new CustomException(System.Net.HttpStatusCode.BadRequest, "Roadmap is null", "Image or markdown is invalid");
            }

            var roadmap = new Roadmap();
            roadmap.CreatedAt = DateTime.UtcNow;

            if (image != null)
            {
                //string path = _appEnvironment.WebRootPath + "\\Resources\\RoadmapImages\\" + $"File{DateTime.UtcNow.ToFileName()}-" + image.FileName;
                string path = "/app/wwwroot" + "/Resources/RoadmapImages/"  + $"File{DateTime.UtcNow.ToFileName()}-" + image.FileName;
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await image.CopyToAsync(fileStream);
                }

                roadmap.ImageName = image.FileName;
                roadmap.Path = path;
            }

            if (markdown != null)
            {
                roadmap.Markdown = Markdown.Parse(markdown);
            }

            await _unitOfWork.Repository<Roadmap>().InsertAsync(roadmap);
            await _unitOfWork.SaveChangesAsync();

            var response = _mapper.Map<RoadmapResponseModel>(roadmap);
            response.Image = File.ReadAllBytes(roadmap.Path);
            return new BaseResponse<RoadmapResponseModel>
            {
                Data = response,
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }

        public IBaseResponse<RoadmapResponseModel> Get(int id)
        {
            var roadmap = _unitOfWork.Repository<Roadmap>().GetById(id);
            if (roadmap == null)
            {
                throw new CustomException(System.Net.HttpStatusCode.NotFound, "Roadmap not found", "Such roadmap does not exist");
            }

            var response = _mapper.Map<RoadmapResponseModel>(roadmap);
            response.Image = File.ReadAllBytes(roadmap.Path);
            return new BaseResponse<RoadmapResponseModel> { Data = response, StatusCode = System.Net.HttpStatusCode.OK };
        }

        public IBaseResponse<MessageResponseModel> Delete(int id)
        {
            var roadmap = _unitOfWork.Repository<Roadmap>().Get(x=> x.Id == id).FirstOrDefault();

            if (roadmap == null)
            {
                throw new CustomException(System.Net.HttpStatusCode.NotFound, "Roadmap not found", "Such roadmap does not exist");
            }

            _unitOfWork.Repository<Roadmap>().DeleteById(id);
            _unitOfWork.SaveChanges();
            return new BaseResponse<MessageResponseModel>() { Data = new MessageResponseModel($"{id} was deleted"), StatusCode = System.Net.HttpStatusCode.OK };
        }

        public async Task<IBaseResponse<RoadmapResponseModel>> Update(int id, RoadmapRequestModel model)
        {
            if (model.Image == null && model.Markdown == null)
            {
                throw new CustomException(System.Net.HttpStatusCode.BadRequest, "Roadmap is null", "Image or markdown is invalid");
            }

            var roadmap = _unitOfWork.Repository<Roadmap>().GetById(id);
            if (roadmap == null)
            {
                throw new CustomException(System.Net.HttpStatusCode.NotFound, "Roadmap not found", "Such roadmap does not exist");
            }

            if (model.Image != null)
            {
                //string path = _appEnvironment.WebRootPath + "\\Resources\\RoadmapImages\\" + $"File{DateTime.UtcNow.ToFileName()}-" + model.Image.FileName;
                string path = "/app/wwwroot" + "/Resources/RoadmapImages/" + $"File{DateTime.UtcNow.ToFileName()}-" + model.Image.FileName;

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await model.Image.CopyToAsync(fileStream);
                }

                roadmap.ImageName = model.Image.FileName;
                roadmap.Path = path;
            }

            if (model.Markdown != null && model.Markdown != "")
            {
                roadmap.Markdown += Markdown.Parse(model.Markdown);
            }

            _unitOfWork.Repository<Roadmap>().Update(roadmap);
            _unitOfWork.SaveChanges();

            var response = _mapper.Map<RoadmapResponseModel>(roadmap);
            response.Image = File.ReadAllBytes(roadmap.Path);
            return new BaseResponse<RoadmapResponseModel> { Data = response, StatusCode = System.Net.HttpStatusCode.OK };
        }

        public async Task<IBaseResponse<IEnumerable<SmallRoadmapResponseModel>>> GetAll()
        {
            var roadmaps = _unitOfWork.Repository<Roadmap>().GetAll();
            if (roadmaps == null)
                throw new CustomException(System.Net.HttpStatusCode.NotFound, "roadmaps not found", "roadmaps is not found");

            var response = _mapper.Map<IEnumerable<SmallRoadmapResponseModel>>(roadmaps);
            return new BaseResponse<IEnumerable<SmallRoadmapResponseModel>>() {Data = response, StatusCode = System.Net.HttpStatusCode.OK};
        }
    }
}
