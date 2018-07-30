using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartAdmin.Seed.Data.Entity
{
    public class ArticleVersions
    {
        public int ID { get; set; }
        public int VersionNumber { get; set; }
        public string ArticleTitle { get; set; }
        public string ArticleContent { get; set; }
        public string Url { get; set; }
        public string ArticleDescription { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
