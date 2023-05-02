using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationAuth.Models.ResponseModels
{
    public class VacancyResponseModel
    {
        public string Title { get; set; }
        public string CompanyName { get; set; }
        public string? CompanyLogoUrl { get; set; }
        public string Content { get; set; }
        public string Employment { get; set; }
        public string? Experience { get; set; }
        public string JobLink { get; set; }
        public string Date { get; set; }
    }
}
