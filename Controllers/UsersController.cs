using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartAdmin.Seed.Extensions;
using SmartAdmin.Seed.Models;
using SmartAdmin.Seed.Models.AccountViewModels;
using SmartAdmin.Seed.Models.ManageViewModels;
using SmartAdmin.Seed.Services;

namespace SmartAdmin.Seed.Controllers
{
    [Authorize]    
    public class UsersController : Controller
    {
        private readonly ILogger _logger;
        private readonly IEmailSender _emailSender;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UrlEncoder _urlEncoder;
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IEmailSender emailSender, ILogger<ManageController> logger,
            UrlEncoder urlEncoder)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _logger = logger;
            _urlEncoder = urlEncoder;
        }
        public IActionResult Index()
        {
            //ViewBag.Title = _userManager.Users.Count();
            var users = _userManager.Users.ToList();
            var model = new List<IndexViewModel>();
            foreach (var user in users)
            {
                var u = new IndexViewModel
                {
                    Username = user.UserName,
                    Email = user.Email,
                    IsEmailConfirmed = user.EmailConfirmed,
                    ID = user.Id,
                    FullName = user.FullName
                };
                model.Add(u);
            }
            return View(model);
        }
        [HttpGet]
        public  IActionResult Create()
        {
            ViewData["Action"] = "Create";
            return  PartialView("_UserData");
        }

        [HttpPost]        
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RegisterViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (!ModelState.IsValid)
                return View(model);

            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                _logger.LogInformation("User created a new account with password.");

                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);
                await _emailSender.SendEmailConfirmationAsync(model.Email, callbackUrl);

                await _signInManager.SignInAsync(user, isPersistent: false);
                
                //return RedirectToLocal(returnUrl);
            }

            // AddErrors(result);

            // If we got this far, something failed, redisplay form            
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{id}'.");
            }


            var model = new IndexViewModel
            {
                Username = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                IsEmailConfirmed = user.EmailConfirmed,
                FullName = user.FullName
            };
            ViewData["Action"] = "Edit";
            return PartialView("_UserData", model);
        }
    }
}
