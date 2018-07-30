using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using Nest;

namespace SmartAdmin.Seed.Data.Entity
{    
    public class Posts
    {               
        public int ID { get; set; }

        [Required]        
        public string PostTitle {
            get;set;
        }
        
        public string PostContent { get; set; }
        
        public string PostDescription { get; set; }

        [DisplayName("Tags")]
        private string _Tags { get; set; }
        [NotMapped]
        public string[] Tags {
            get { return _Tags.Split(","); }
            set { _Tags = string.Join(",", value);  }
        }
        
        public string PostUrl { get; set; }
                
        public DateTime PublishedDate { get; set; }
        
        public DateTime LastModified { get; set; }


    }
}
