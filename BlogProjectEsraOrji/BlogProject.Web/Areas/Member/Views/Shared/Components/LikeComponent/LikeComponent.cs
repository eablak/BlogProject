using BlogProject.Infrastructure.Repositories.Interfaces.EntityTypeRepository;
using BlogProject.Web.Areas.Member.Models.DTOs;
using BlogProject.Web.Areas.Member.Models.ViewEntity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace BlogProject.Web.Areas.Member.Views.Shared.Components.LikeComponent
{
    [ViewComponent(Name = "LikeComponent")]
    public class LikeComponent : ViewComponent
    {
        private readonly ILikeRepository likeRepository;
        private readonly UserManager<IdentityUser> userManager;

        public LikeComponent(ILikeRepository likeRepository, UserManager<IdentityUser> userManager)
        {
            this.likeRepository = likeRepository;
            this.userManager = userManager;
        }


        public IViewComponentResult Invoke()
        {
            var idmakale = TempData["makaleid"];
            var idkullanici = TempData["kullaniciid"];

            List<LikeDetailWithUser> likes = likeRepository.ListCount(a => a.ArticleId == (int)idmakale).Select(a => new LikeDetailWithUser
            {
                ArticleId = a.ArticleId,
                AppUserId = a.AppUserId,
            }).ToList();

            return View(likes);
        }
    }
}
