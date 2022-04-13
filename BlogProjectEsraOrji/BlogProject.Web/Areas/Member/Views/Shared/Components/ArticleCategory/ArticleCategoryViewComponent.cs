using BlogProject.Infrastructure.Repositories.Interfaces.EntityTypeRepository;
using BlogProject.Web.Areas.Member.Models.VMs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace BlogProject.Web.Areas.Member.Views.Shared.Components.ArticleCategory
{
    public class ArticleCategoryViewComponent : ViewComponent
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IArticleRepository articleRepository;

        public ArticleCategoryViewComponent(ICategoryRepository categoryRepository, IArticleRepository articleRepository)
        {
            this.categoryRepository = categoryRepository;
            this.articleRepository = articleRepository;
        }

        public IViewComponentResult Invoke()
        {
            List<ArticleCategoryVM> mylist = categoryRepository.GetDefaults(a => a.Statu == Model.Enums.Statu.Active).Select(a => new ArticleCategoryVM { CategoryName = a.Name, CategoryId = a.Id, IsSelected = false }).ToList();
            return View(mylist);
        }
    }
}
