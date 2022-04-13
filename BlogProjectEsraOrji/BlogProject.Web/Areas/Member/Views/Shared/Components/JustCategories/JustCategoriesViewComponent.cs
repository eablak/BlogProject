using BlogProject.Infrastructure.Repositories.Interfaces.EntityTypeRepository;
using BlogProject.Web.Areas.Member.Models.VMs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace BlogProject.Web.Areas.Member.Views.Shared.Components.JustCategories
{
    [ViewComponent(Name = "JustCategories")]
    public class JustCategoriesViewComponent : ViewComponent
    {
        private readonly IArticleCategoryRepository articleCategoryRepository;

        public JustCategoriesViewComponent(IArticleCategoryRepository articleCategoryRepository)
        {
            this.articleCategoryRepository = articleCategoryRepository;
        }

        public IViewComponentResult Invoke()
        {
            var idmakale = TempData["makaleid"];

            List<OnlyCategoriesVM> listem = articleCategoryRepository.GetDefaults(a => a.ArticleID == (int)idmakale).Select(a => new OnlyCategoriesVM { CategoryID = a.CategoryID, CategoryName = a.Category.Name }).ToList();

            return View(listem);
        }

    }
}
