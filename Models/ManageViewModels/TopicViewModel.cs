using SmartAdmin.Seed.Data.Entity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmartAdmin.Seed.Models.ManageViewModels
{
    public class TopicViewModel
    {
        public int ID { get; set; }
        public string TopicName { get; set; }
        public string Description { get; set; }
        public string UserCreated { get; set; }
        public DateTime DateCreated { get; set; }

        //[DisplayFormat(DataFormatString = @"{0:dd\/mm\/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        //[DisplayFormat(DataFormatString = @"{0:dd\/mm\/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }
        public string Rules { get; set; }
        public string Status { get; set; }
        public bool isFeatured { get; set; }
        public string SelectedSources { get; set; }
        public int NoOfPosts { get; set; }
        public int page { get; set; }
        public int size { get; set; }
        public IReadOnlyCollection<Articles> Articles { get; set; }
    }
}
