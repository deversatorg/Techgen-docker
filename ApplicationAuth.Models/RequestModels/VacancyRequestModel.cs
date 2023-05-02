using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationAuth.Models.RequestModels
{
    public class VacancyRequestModel
    {
        public string SearchQuery { get; set; }
        public string Employment { get; set; }
        public string Direction { get; set; }
    }
}
