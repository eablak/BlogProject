using AutoMapper;
using BlogProject.Infrastructure.Repositories.Interfaces.EntityTypeRepository;
using BlogProject.Model.Entities.Concrete;
using BlogProject.Web.Areas.Member.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogProject.Web.Areas.Member.Controllers
{
    [Area("Member")]
    [Authorize(Roles = "Member")] // yetkilendirme yaparken sadece rolü member olan buraya gelebilir.
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IMapper mapper;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IAppUserRepository appUserRepository;

        public CategoryController(ICategoryRepository categoryRepository, IMapper mapper, UserManager<IdentityUser> userManager, IAppUserRepository appUserRepository)
        {
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
            this.userManager = userManager;
            this.appUserRepository = appUserRepository;
        }


        public IActionResult Create() => View();


        [HttpPost]
        public IActionResult Create(CreateCategoryDTO model)
        {
            if (ModelState.IsValid)
            {
                var category = mapper.Map<Category>(model);  // eger mappingde (mappers ) yazmamıssak hata verirdi
                category.IsActive = false;
                categoryRepository.Create(category);
                return RedirectToAction("List");
            }
            return View(model);
        }


        public IActionResult List()
        {
            List<Category> list = categoryRepository.GetDefaults(a => a.IsActive != false);    
            return View(list);
        }


        [HttpGet]
        public IActionResult Update(int id)
        {
            Category category = categoryRepository.GetDefault(a => a.Id == id);
            var updateCategoryObj = mapper.Map<UpdateCategoryDTO>(category);
            return View(updateCategoryObj);

        }

        [HttpPost]

        public IActionResult Update(UpdateCategoryDTO model)
        {
            if (ModelState.IsValid)
            {
                var category = mapper.Map<Category>(model);
                //category.IsActive = true;
                categoryRepository.Update(category);
                return RedirectToAction("List");
            }
            return View(model);

        }

        public IActionResult Detail(int id)
        {
            Category category = categoryRepository.GetDefault(a => a.Id == id);
            return View(category);
        }

        public IActionResult Delete(int id)
        {
            Category category = categoryRepository.GetDefault(a => a.Id == id);
            categoryRepository.Delete(category);
            return RedirectToAction("List");
        }


        public async Task<IActionResult> Follow(int id)
        {
            IdentityUser user = await userManager.GetUserAsync(User);
            AppUser appUser = appUserRepository.GetDefault(a => a.IdentityId == user.Id);

            Category category = categoryRepository.GetDefault(a => a.Id == id);

            category.UserFollowedCategories.Add(new UserFollowedCategory() { AppUser = appUser, AppUserID = appUser.Id, Category = category, CategoryID = category.Id });
            categoryRepository.Update(category);
            return RedirectToAction("List");

        }

        public async Task<IActionResult> Unfollow(int id)
        {
            IdentityUser user = await userManager.GetUserAsync(User);
            AppUser appUser = appUserRepository.GetDefault(a => a.IdentityId == user.Id);

            Category category = categoryRepository.GetDefault(a => a.Id == id);

            if (categoryRepository.CategoryUnFollowed(appUser.Id,category))
            {
                return RedirectToAction("List");
            }

            return RedirectToAction("List");
        }

    }
}
