using SmartAdmin.Seed.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartAdmin.Seed.Models.Search
{
    public class SearchResultViewModel
    {
        public long Total { get; set; }
        public long Took { get; set; }
        public string SearchTerm { get; set; }
        public IReadOnlyCollection<Articles> Results { get; set; }
    }
}
