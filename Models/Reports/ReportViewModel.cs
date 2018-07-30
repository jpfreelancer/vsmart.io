using SmartAdmin.Seed.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartAdmin.Seed.Models.Reports
{
    public class ReportViewModel
    {
        public string ReportTitle { get; set; }
        public long Total { get; set; }
        public long Took { get; set; }        
        public int TopicID { get; set; }
        public string TopicName { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }        
        public IReadOnlyCollection<Articles> Results { get; set; }
    }
}
