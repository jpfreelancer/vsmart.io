using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SmartAdmin.Seed.Data.Entity
{
    public class Sources
    {
        public int ID { get; set; }

        [Required]
        public string SourceName { get; set; }

        public int SourceTypeId { get; set; }

        public enum SourceType
        {
            News, Forum, Blog, Facebook, Twitter
        }

        [Required]
        public string SourceUrl { get; set; }

        
        public ICollection<Articles> Articles { get; set; }


    }
}
