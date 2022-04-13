using BlogProject.Infrastructure.Repositories.Interfaces.EntityTypeRepository;
using BlogProject.Web.Areas.Member.Models.ViewEntity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogProject.Web.Views.Shared.Components.Articles
{
    [ViewComponent(Name = "Articles")]
    public class ArticleViewComponent : ViewComponent
    {
        private readonly IArticleRepository articleRepository;

        public ArticleViewComponent(IArticleRepository articleRepository)
        {
            this.articleRepository = articleRepository;
        }


        public IViewComponentResult Invoke()
        {
            List<ArticleDetailWithUser> articles = articleRepository.GetArticleWithUser(a => a.IsActive==true).OrderByDescending(a => a.CreateDate)
                .Select(a => new ArticleDetailWithUser
                {
                    UserId = a.AppUserId,
                    CreateDate = a.CreateDate,
                    Content = a.Content,
                    Title = a.Title,
                    ArticleId = a.Id,
                    UserFullName = a.AppUser.FullName,
                    Image = a.AppUser.Image


                }).Take(10).ToList();

            return View(articles);

        }


    }
}
