using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using jsreport.AspNetCore;
using jsreport.Client;
using jsreport.Types;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nest;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SmartAdmin.Seed.Data;
using SmartAdmin.Seed.Data.Entity;
using SmartAdmin.Seed.Extensions;
using SmartAdmin.Seed.Models.Reports;

namespace SmartAdmin.Seed.Controllers
{
    [Authorize]
    public class ReportController : Controller
    {
        private readonly IReportingService _rs;
        private readonly IElasticClient _client;
        private readonly ApplicationDbContext _context;
        public ReportController(IReportingService rs, IElasticClient client, ApplicationDbContext context)
        {
            _rs = rs;
            _client = client;
            _context = context;
        }
        
        public IActionResult Index()
        {
            
            return View(_context.Topics.ToList());
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[MiddlewareFilter(typeof(JsReportPipeline))]
        public async Task<IActionResult> ReportByTopics(int ID, string ReportTitle, string StartDate, string EndDate)
        {
            var model = new ReportViewModel();
            var topic = await _context.Topics.SingleOrDefaultAsync(s => s.ID == ID);
            if (topic == null)
            {
                throw new ApplicationException($"Unable to load topic with ID '{ID}'.");
            }


            var request = new SearchRequest
            {
                Query = ElasticsearchQueryBuilder.QueryBuilder(JObject.Parse(topic.Rules)),
                PostFilter = new QueryContainer(
                    new DateRangeQuery
                    {
                        Field = "modified",
                        Format = "dd-MM-YYYY",
                        GreaterThanOrEqualTo = StartDate,
                        LessThanOrEqualTo = EndDate
                    }),
                Sort = new List<ISort>
                    {
                        new SortField { Field = "modified", Order = SortOrder.Descending },
                        new SortField { Field = "_score", Order = SortOrder.Descending }
                    },
                
            };
            var response = await _client.SearchAsync<Articles>(request);

            if (ModelState.IsValid)
            {
                var data = new
                {
                    reporttitle = ReportTitle,
                    startdate = StartDate,
                    enddate = EndDate,
                    topicname = topic.TopicName,
                    took = response.Took,
                    total = response.Total,
                    articles = response.Documents

                };
                var report = await _rs.RenderAsync(new RenderRequest()
                {
                    Template = new Template()
                    {
                        Name = "report-by-topic"
                    },
                    Data = JsonConvert.SerializeObject(data)
                });
                Debug.WriteLine(JsonConvert.SerializeObject(data));
                //HttpContext.
                return new FileStreamResult(report.Content, "application/pdf");
                //return report.Content

            }
            //HttpContext.JsReportFeature().Recipe(Recipe.PhantomPdf);

            return RedirectToAction("Index");
        }
        
    }
}
