using BlogProject.Infrastructure.Repositories.Interfaces.EntityTypeRepository;
using BlogProject.Web.Areas.Member.Models.VMs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogProject.Web.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IArticleRepository articleRepository;

        public ArticleController(IArticleRepository articleRepository)
        {
            this.articleRepository = articleRepository;
        }
        public IActionResult Index()
        {
            return View();
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
                    CreateDate = a.CreateDate
                },
                expression: a => a.Id==id,
                include: a => a.Include(b => b.AppUser));
            
            return View(article);
        }
    }
}
