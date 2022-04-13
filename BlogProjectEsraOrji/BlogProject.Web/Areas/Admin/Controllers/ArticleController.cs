using BlogProject.Infrastructure.Repositories.Interfaces.EntityTypeRepository;
using BlogProject.Model.Entities.Concrete;
using BlogProject.Web.Areas.Member.Models.VMs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BlogProject.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ArticleController : Controller
    {
        private readonly IArticleRepository articleRepository;

        public ArticleController(IArticleRepository articleRepository)
        {
            this.articleRepository = articleRepository;
        }

        public IActionResult List()
        {
            List<Article> list = articleRepository.GetDefaults(a => a.IsActive == true);
            return View(list);
        }

        public IActionResult Passive(int id)
        {
            Article article = articleRepository.GetDefault(a => a.Id == id);
            article.IsActive = false;
            articleRepository.Update(article);
            return RedirectToAction("List");
        }

        public IActionResult Detail(int id)
        {
            var article = articleRepository.GetByDefault(
                selector: a => new GetArticleDetailVM()
                {
                    ArticleId = a.Id,
                    Title = a.Title,
                    Content = a.Content,
                    Image = a.Image,
                    AuthorName = a.AppUser.FullName,
                    AuthorImage = a.AppUser.Image,
                    CreateDate = a.CreateDate,
                    View = a.viewCount,
                    minute = articleRepository.ArticleTime(id)

                },
                expression: a => a.Id == id,
                include: a => a.Include(b => b.AppUser));
            return View(article);
        }
    }
}
