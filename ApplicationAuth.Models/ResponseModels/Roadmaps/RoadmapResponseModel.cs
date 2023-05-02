using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationAuth.Models.ResponseModels.Roadmaps
{
    public class RoadmapResponseModel
    {
        public int Id { get; set; }
        public string ImageName { get; set; }
        public byte[] Image { get; set; }
        public string Markdown { get; set; }
    }
}
