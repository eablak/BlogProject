using BlogProject.Infrastructure.Repositories.Interfaces.EntityTypeRepository;
using BlogProject.Model.Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BlogProject.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IAppUserRepository appUserRepository;

        public UserController(ICategoryRepository categoryRepository,IAppUserRepository appUserRepository)
        {
            this.categoryRepository = categoryRepository;
            this.appUserRepository = appUserRepository;
        }       

        public IActionResult List()
        {
            List<AppUser> list = appUserRepository.GetDefaults(a => a.IsActive == false);
            return View(list);
        }

        public IActionResult Accept(int id)
        {
            AppUser appUser = appUserRepository.GetDefault(a => a.Id == id);
            appUser.IsActive = true;
            appUserRepository.Update(appUser);
            return RedirectToAction("List");
        }

        public IActionResult Delete(int id)
        {
            AppUser appUser = appUserRepository.GetDefault(a => a.Id == id);
            appUserRepository.Delete(appUser);
            return RedirectToAction("List");
        }

    }
}
