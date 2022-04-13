using BlogProject.Infrastructure.Repositories.Interfaces.EntityTypeRepository;
using BlogProject.Model.Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BlogProject.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = "Admin")] Bunu yapınca bende giremiyorum
    public class AdminController : Controller
    {
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IAppUserRepository appUserRepository;

        public AdminController(SignInManager<IdentityUser> signInManager,IAppUserRepository appUserRepository)
        {
            this.signInManager = signInManager;
            this.appUserRepository = appUserRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();
            return Redirect("~/");
        }

        public IActionResult HomePage()
        {
            return RedirectToAction("Index");
        }
    }
}
