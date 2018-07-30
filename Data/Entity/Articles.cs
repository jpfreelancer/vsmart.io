using Nest;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SmartAdmin.Seed.Data.Entity
{
    [ElasticsearchType(IdProperty = nameof(ID))]
    public class Articles
    {
        //Data for index
        [NotMapped]        
        public Guid ID { get; set; }
                    
        [NotMapped]
        [Text(Name = "title")]
        public string[] Title { get; set; }

        [NotMapped]
        [Text(Name = "content")]
        public string[] Content { get; set; }
        [NotMapped]
        [Text(Name = "description")]
        public string[] Description { get; set; }

        [NotMapped]
        [Keyword(Name = "url")]
        public string[] Url { get; set; }

        [NotMapped]
        [Date(Name = "modified")]
        public DateTime[] Modified { get; set; }

        [NotMapped]
        [Keyword(Name = "imageurl")]
        public string[] ImageUrl { get; set; }

        //Data for database mapping
        public int articlesID { get; set; }

        [Required]
        public string articleTitle
        {
            get; set;
        }

        public string articleContent { get; set; }

        public string articleDescription { get; set; }

        
        public string articleUrl { get; set; }

        public string imageUrl { get; set; }        

        public DateTime publishedDate { get; set; }

        public DateTime lastModified { get; set; }
    }
}
