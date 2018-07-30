using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SmartAdmin.Seed.Data.Entity
{
    public class Topics
    {
        public int ID { get; set; }
        public string TopicName { get; set; }
        public string Description { get; set; }
        public string UserCreated { get; set; }
        public DateTime DateCreated  { get; set; }
        //[DisplayFormat(DataFormatString = "{0:dd/mm/yy}")]
        public DateTime StartDate { get; set; }
        //[DisplayFormat(DataFormatString = "{0:dd/mm/yy}")]
        public DateTime EndDate { get; set; }
        public string Rules { get; set; }
        public string ESRequest { get; set; }
        public string Status { get; set; }
        public bool isFeatured { get; set; }
        public string SelectedSources { get; set; }        
    }
}
