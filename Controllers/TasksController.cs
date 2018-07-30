using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nest;
using SmartAdmin.Seed.Data;
using SmartAdmin.Seed.Data.Entity;
using SmartAdmin.Seed.Models.ManageViewModels;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using SmartAdmin.Seed.Models;
using jsreport.AspNetCore;
using jsreport.Types;

namespace SmartAdmin.Seed.Controllers
{
    [Authorize]
    public class TasksController : Controller
    {
        private readonly ILogger _logger;
        private readonly ApplicationDbContext _context;
        private readonly IElasticClient _client;
        private readonly UserManager<ApplicationUser> _userManager;
        //public IJsReportMVCService JsReportMVCService { get; }
        public TasksController(ILogger<ManageController> logger, ApplicationDbContext context, IElasticClient client, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _context = context;
            _client = client;
            _userManager = userManager;
            //JsReportMVCService = jsReportMVCService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var tasks = await _context.Tasks.OrderByDescending(d => d.DateCreated).ToListAsync();
            var model = new List<TaskViewModel>();
            foreach (var task in tasks)
            {
                var user = _userManager.FindByIdAsync(task.UserID);
                
                var t = new TaskViewModel
                {
                    ID = task.ID,
                    Description = task.Description,
                    DateCreated = task.DateCreated,
                    UserName = user.Result.UserName,
                    LastModified = task.LastModified,
                    Status = task.Status,
                    Priority = task.Priority,
                    Frequency = task.Frequency,
                    Histories = task.Histories,
                    ArticleVersions = task.ArticleVersions,
                    ArticleTitle = task.ArticleTitle,
                    ArticleContent = task.ArticleContent,
                    ArticleDescription = task.ArticleDescription,
                    ArticleUrl = task.Url

                };
                model.Add(t);
            }                
            
            return View(model);
        }
        [HttpGet]
        
        public IActionResult Create(string url, string title, int topicID)
        {            
            var model = new TaskViewModel
            {
                ArticleTitle = title,
                ArticleUrl = url,
                TopicID = (int)topicID
            };
            
            return PartialView("_TaskData", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ArticleUrl,Description,Priority,Frequency, TopicID")] TaskViewModel model)
        {
            //ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var request = new SearchRequest
                {
                    Query = new TermQuery
                    {
                        Field = "url",
                        Value = System.Net.WebUtility.UrlDecode(model.ArticleUrl)
                    }
                };
                var response = await _client.SearchAsync<Articles>(request);
                //response.
                var article = response.Hits.First();
                //var json = _client.RequestResponseSerializer.SerializeToString(request);
                if (article == null)
                {
                    throw new ApplicationException($"Unable to load article with url '{model.ArticleUrl}'.");
                }                
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
                }
                var version = new ArticleVersions
                {
                    ArticleContent = article.Source.Content[0],
                    ArticleTitle = article.Source.Title[0],
                    ArticleDescription = article.Source.Description[0],
                    Url = article.Source.Url[0],
                    DateCreated = DateTime.Now,
                    VersionNumber = 1
                };
                var task = new Tasks
                {
                    ArticleContent = article.Source.Content[0],
                    ArticleTitle = article.Source.Title[0],
                    ArticleDescription = article.Source.Description[0],
                    Url = article.Source.Url[0],
                    UserID = _userManager.GetUserId(User),
                    ImageUrl = article.Source.ImageUrl[0],
                    Description = model.Description,
                    Priority = model.Priority,
                    Frequency = model.Frequency,
                    DateCreated = DateTime.Now,
                    LastModified = DateTime.Now,
                    Status = "Active",
                    ArticleVersions = new List<ArticleVersions>()
                    {
                        version
                    }
                };
                
                try
                {
                    _context.Update(task);
                    await _context.SaveChangesAsync();
                    ViewData["SuccessMessage"] = "Thêm mới tác vụ thành công!";
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
            }

            return RedirectToAction("Single","Topics", new { ID = model.TopicID });
        }

        [HttpGet]
        
        public async Task<IActionResult> TaskView(int ID)
        {
            var task = await _context.Tasks
                .Include(t => t.ArticleVersions)
                .SingleOrDefaultAsync(s => s.ID == ID);
            if (task == null)
            {
                throw new ApplicationException($"Unable to load task with ID '{ID}'.");
            }
            var model = new TaskViewModel
            {
                ArticleContent = task.ArticleContent,
                ArticleTitle = task.ArticleTitle,
                ArticleDescription = task.ArticleDescription,
                ArticleUrl = task.Url,
                ArticleVersions = task.ArticleVersions,
                UserName = _userManager.FindByIdAsync(task.UserID).Result.UserName,
                ArticleImageUrl = task.ImageUrl,
                Description = task.Description,
                Priority = task.Priority,
                Frequency = task.Frequency,
                DateCreated = task.DateCreated,
                LastModified = task.LastModified,
                Status = task.Status
            };
            
            return View(model);
        }
        [HttpGet]
        
        public IActionResult ArticleVersionsView()
        {
            return PartialView("_ArticleVersionsComparison");
        }
    }
}
