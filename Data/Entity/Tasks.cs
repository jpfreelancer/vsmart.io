using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartAdmin.Seed.Data.Entity
{
    public class Tasks
    {
        public int ID { get; set; }
        public string ArticleTitle { get; set; }
        public string ArticleDescription { get; set; }
        public string ArticleContent { get; set; }
        public string Url { get; set; }
        public string ImageUrl { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime LastModified { get; set; }
        public string UserID { get; set; }
        //[DisplayFormat(DataFormatString = "{0:dd/mm/yy}")]
        public string Description { get; set; }
        public string Status { get; set; }
        public string Priority { get; set; }
        public string Frequency { get; set; }
        public List<TaskHistory> Histories { get; set; }
        public List<ArticleVersions> ArticleVersions { get; set; }

    }
}
