using BlogProject.Infrastructure.Repositories.Interfaces.EntityTypeRepository;
using BlogProject.Model.Entities.Concrete;
using BlogProject.Web.Areas.Member.Models.ViewEntity;
using BlogProject.Web.Areas.Member.Models.VMs;
using BlogProject.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BlogProject.Web.Controllers
{
    public class HomeController : Controller
    {

        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly ICategoryRepository categoryRepository;
        private readonly IAppUserRepository appUserRepository;
        private readonly IArticleRepository articleRepository;

        public HomeController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ICategoryRepository categoryRepository, IAppUserRepository appUserRepository, IArticleRepository articleRepository)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.categoryRepository = categoryRepository;
            this.appUserRepository = appUserRepository;
            this.articleRepository = articleRepository;
        }


        [HttpGet("Login")]
        public IActionResult Login()
        {
            return View();
        }

        
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (ModelState.IsValid)
            {
                IdentityUser ıdentityUser = await userManager.FindByEmailAsync(loginVM.Mail);

                AppUser appUser = appUserRepository.GetDefault(a => a.IdentityId == ıdentityUser.Id);
                //if (appUser.Role == Model.Enums.Role.Member)
                //{
            
                if (ıdentityUser != null && appUser.IsActive == true)
                {
                    if (appUser.Statu != Model.Enums.Statu.Passive)
                    {
                        await signInManager.SignOutAsync();
                        Microsoft.AspNetCore.Identity.SignInResult signInResult = await signInManager.PasswordSignInAsync(ıdentityUser, loginVM.Password, false, false);
                        if (signInResult.Succeeded && appUser.Role == Model.Enums.Role.Member)
                        {
                            string role = (await userManager.GetRolesAsync(ıdentityUser)).FirstOrDefault();
                            return RedirectToAction("Index", "AppUser", new { area = role });
                        }
                        if (signInResult.Succeeded && appUser.Role == Model.Enums.Role.Admin)
                        {
                            //string role = (await userManager.GetRolesAsync(ıdentityUser)).FirstOrDefault();
                            string role2 = appUser.Role.ToString();
                            return RedirectToAction("Index", "Admin", new { area = role2 });
                            //appuser diyince member/Appuser'a götürüyor. ama böyle member/admin diyip hata!
                        }
                        else
                        {
                            ModelState.AddModelError("userNotFound", "Kullanıcı bilgi/bilgileri hatalı /eksik.");
                        }
                    }
                   
                }
                //}


            }
            return View(loginVM);
        }



        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AboutUs()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet("CategoryList")]
        public IActionResult CategoryList()
        {
            List<Category> list = categoryRepository.GetDefaults(a => a.Statu != Model.Enums.Statu.Passive);
            return View(list);
        }


        public IActionResult AuthorDetail(int id)
        {

            AppUser author = appUserRepository.GetDefault(a => a.Id == id);
            return View(author);
        }

        public IActionResult ArticleByCategory(int id)
        {
            List<ArticleDetailWithUser> articles = articleRepository.GetArticleWithUser(a => a.Statu != Model.Enums.Statu.Passive && a.ArticleCategory.CategoryID == id).OrderByDescending(a => a.CreateDate)
                .Select(a => new ArticleDetailWithUser
                {
                    UserId = a.AppUserId,
                    CreateDate = a.CreateDate,
                    Content = a.Content,
                    Title = a.Title,
                    ArticleId = a.Id,
                    UserFullName = a.AppUser.FullName,
                    Image = a.AppUser.Image


                }).Take(5).ToList();

            return View(articles);
        }
    }
}
