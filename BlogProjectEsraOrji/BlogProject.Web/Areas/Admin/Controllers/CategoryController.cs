using AutoMapper;
using BlogProject.Infrastructure.Repositories.Interfaces.EntityTypeRepository;
using BlogProject.Model.Entities.Concrete;
using BlogProject.Web.Areas.Member.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BlogProject.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IMapper mapper;

        public CategoryController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
        }

        public IActionResult List()
        {
            List<Category> list = categoryRepository.GetDefaults(a => a.IsActive == false);
            return View(list);
        }

        public IActionResult Delete(int id)
        {
            Category category = categoryRepository.GetDefault(a => a.Id == id);
            categoryRepository.Delete(category);
            return RedirectToAction("List");
        }

        public IActionResult Accept(int id)
        {
            Category category = categoryRepository.GetDefault(a => a.Id == id);
            category.IsActive = true;
            categoryRepository.Update(category);
            return RedirectToAction("List");
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
                categoryRepository.Update(category);
                return RedirectToAction("List");
            }
            return View(model);

        }
    }
}
