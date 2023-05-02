using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationAuth.Models.ResponseModels.Port
{
    public class SmallPostResponseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int LikesCount { get; set; }
    }
}
