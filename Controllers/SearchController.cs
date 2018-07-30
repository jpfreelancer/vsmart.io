using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using jsreport.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nest;
using SmartAdmin.Seed.Data.Entity;
using SmartAdmin.Seed.Models.Search;

namespace SmartAdmin.Seed.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class SearchController : Controller
    {
        private readonly IElasticClient _client;
        public SearchController(IElasticClient client)
        {
            _client = client;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        
        public IActionResult Index(string SearchTerm)
        {
            var response =  _client.Search<Articles>(s => s
                .Index("articles")
                .Type("doc")
                .Size(50)
                .Query(q => q
                    .MatchPhrase(m => m
                        .Field(f => f.Description)                        
                        .Query(SearchTerm)                    
                    )
                )                
            );
            // var rawQuery = Encoding.UTF8.GetString(response.RequestInformation.Request);

            var model = new SearchResultViewModel
            {
                Took = response.Took,
                Total = response.Total,
                Results = response.Documents,
                SearchTerm = SearchTerm
                
            };
            
            return View(model);
        }

    }
}
