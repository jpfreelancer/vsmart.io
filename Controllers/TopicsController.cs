using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartAdmin.Seed.Models.ManageViewModels;
using Microsoft.Extensions.Logging;
using SmartAdmin.Seed.Data;
using SmartAdmin.Seed.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Nest;
using Newtonsoft.Json.Linq;
using Elasticsearch.Net;
using SmartAdmin.Seed.Extensions;

namespace SmartAdmin.Seed.Controllers
{
    [Authorize]    
    public class TopicsController : Controller
    {
        private readonly ILogger _logger;
        private readonly ApplicationDbContext _context;
        private readonly IElasticClient _client;

        [TempData]
        public string StatusMessage { get; set; }
        public TopicsController(ILogger<ManageController> logger, ApplicationDbContext context, IElasticClient client)
        {
            _logger = logger;
            _context = context;
            _client = client;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var topics = await _context.Topics.OrderByDescending(d => d.DateCreated).OrderByDescending(s => s.isFeatured).ToListAsync();
            var model = new List<TopicViewModel>();
            foreach (var topic in topics)
            {
                var request = topic.ESRequest;
                var result = await _client.LowLevel.SearchAsync<SearchResponse<Articles>>(request);
                var t = new TopicViewModel
                {
                    ID = topic.ID,
                    TopicName = topic.TopicName,
                    Description = topic.Description,
                    DateCreated = topic.DateCreated,
                    StartDate = topic.StartDate,
                    EndDate = topic.EndDate,
                    isFeatured = topic.isFeatured,
                    Rules = topic.Rules,
                    SelectedSources = topic.SelectedSources,
                    //Status = topic.Status,
                    NoOfPosts = (int)result.Total                    
                    
                };
                model.Add(t);
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["Action"] = "Create";
            return PartialView("_TopicData");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TopicName,Description,StartDate,EndDate, isFeatured, Rules")] TopicViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var rules = model.Rules;
                var request = new SearchRequest
                {
                    Query = ElasticsearchQueryBuilder.QueryBuilder(JObject.Parse(rules)),
                    Size = 50
                   
                };
                var topic = new Topics
                {
                    TopicName = model.TopicName,
                    Description = model.Description,
                    DateCreated = DateTime.Now,
                    StartDate = model.StartDate,
                    EndDate = model.EndDate,
                    Rules = model.Rules,
                    ESRequest = _client.RequestResponseSerializer.SerializeToString(request),
                    isFeatured = model.isFeatured
                };                               
                try
                {
                    _context.Add(topic);
                    var result = await _context.SaveChangesAsync();
                    
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
            }
            StatusMessage = "Thêm mới chủ đề thành công!";
            return RedirectToAction(nameof(Index));
            //return View("Index", model);
        }
        // POST: Topics/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? ID, [Bind("ID,TopicName,Description,Rules, isFeatured")] TopicViewModel model)
        {
            
            var topic = await _context.Topics.SingleOrDefaultAsync(s => s.ID == ID);
            if (topic == null)
            {
                throw new ApplicationException($"Unable to load topic with ID '{ID}'.");
            }
            if (ModelState.IsValid)
            {
                var request = new SearchRequest
                {
                    Query = ElasticsearchQueryBuilder.QueryBuilder(JObject.Parse(model.Rules)),
                    Size = 50
                   
                };
                try
                {
                    topic.TopicName = model.TopicName;
                    topic.Description = model.Description;                    
                    topic.isFeatured = model.isFeatured;
                    topic.Rules = model.Rules;
                    topic.ESRequest = _client.RequestResponseSerializer.SerializeToString(request);
                    _context.Update(topic);
                    await _context.SaveChangesAsync();
                    
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
            }
            TempData["SuccessMessage"] = "Cập nhật chủ đề thành công!";
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]        
        public async Task<IActionResult> Edit(int? ID)
        {
            var topic = await _context.Topics.SingleOrDefaultAsync(s => s.ID == ID);
            if (topic == null)
            {
                throw new ApplicationException($"Unable to load topic with ID '{ID}'.");
            }
            var model = new TopicViewModel
            {
                ID = topic.ID,
                TopicName = topic.TopicName,
                Description = topic.Description,                
                isFeatured = topic.isFeatured,
                Rules = topic.Rules
            };
            ViewData["Action"] = "Edit";
            return PartialView("_TopicData", model);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int? ID)
        {
            var topic = await _context.Topics.SingleOrDefaultAsync(s => s.ID == ID);
            if (topic == null)
            {
                throw new ApplicationException($"Unable to load topic with ID '{ID}'.");
            }
            ViewData["ID"] = ID;
            ViewData["TopicName"] = topic.TopicName;
            return PartialView("_DeleteConfirm");
            //return 
        }
        //Actual delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? ID)
        {
            var topic = await _context.Topics
                .AsNoTracking()
                .SingleOrDefaultAsync(s => s.ID == ID);
            if (topic == null)
            {
                throw new ApplicationException($"Unable to load topic with ID '{ID}'.");
            }
            try
            {
                _context.Topics.Remove(topic);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Đã xóa chủ đề thành công!";
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction(nameof(Index));
            }

        }
        //Single topic view
        [HttpGet]
        public async Task<IActionResult> Single(int? ID)
        {
            if (ID == null)
            {
                return NotFound();
            }
            var topic = await _context.Topics.SingleOrDefaultAsync(s => s.ID == ID);
            if (topic == null)
            {
                throw new ApplicationException($"Unable to load topic with ID '{ID}'.");
            }
            ViewData["ID"] = ID;
            ViewData["TopicName"] = topic.TopicName;
            ViewData["Page"] = 1;
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetArticles(int? ID, int page = 1)
        {
            if (ID == null)
            {
                return NotFound();
            }            
            var topic = await _context.Topics.SingleOrDefaultAsync(s => s.ID == ID);
            if (topic == null)
            {
                throw new ApplicationException($"Unable to load topic with ID '{ID}'.");
            }
            //var request = topic.ESRequest;            
            int size = 20;
            int from = (page - 1) * size;
            

            var request = new SearchRequest
            {
                Query = ElasticsearchQueryBuilder.QueryBuilder(JObject.Parse(topic.Rules)),
                Sort = new List<ISort>
                    {
                        new SortField { Field = "modified", Order = SortOrder.Descending },
                        new SortField { Field = "_score", Order = SortOrder.Descending }
                    },
                Size = size,
                From = from
            };
            var result = await  _client.SearchAsync<Articles>(request);
            //bool loadmore = (page * size <= (int)result.Total);
            //var esreq = _client.RequestResponseSerializer.SerializeToString(request);
            var model = new TopicViewModel
            {
                ID = (int)ID,
                TopicName = topic.TopicName,
                NoOfPosts = (int)result.Total,
                page = page,
                size = size,
                Articles = result.Documents
        };
            ViewData["Page"] = page;
            //ViewData["More"] = loadmore;
            return PartialView("_ArticleList", model);
        }

        [HttpGet]
        public IActionResult ArticleView(string content)
        {
            ViewData["Content"] = content;
            
            return PartialView("_ArticleView", ViewData["Content"]);
        }

       
    }
}
