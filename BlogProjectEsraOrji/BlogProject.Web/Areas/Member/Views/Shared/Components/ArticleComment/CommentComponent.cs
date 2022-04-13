using BlogProject.Infrastructure.Repositories.Interfaces.EntityTypeRepository;
using BlogProject.Model.Entities.Concrete;
using BlogProject.Web.Areas.Member.Models.ViewEntity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace BlogProject.Web.Areas.Member.Views.Shared.Components.CommentByArticle
{
    [ViewComponent(Name = "ArticleComment")]
    public class CommentComponent : ViewComponent
    {
        private readonly ICommentRepository commentRepository;
        private readonly UserManager<IdentityUser> userManager;

        public CommentComponent(ICommentRepository commentRepository, UserManager<IdentityUser> userManager)
        {
            this.commentRepository = commentRepository;
            this.userManager = userManager;
        }

        public IViewComponentResult Invoke()
        {
            var idmakale = TempData["makaleid"];
            var idkullanici = TempData["kullaniciid"];
            //IdentityUser user = await userManager.GetUserAsync(User);
            //AppUser appUser = appUserRepository.GetDefault(a => a.IdentityId == user.Id);

            List<CommentDetailWithUser> readcomments = commentRepository.CommentUserArticle(a => a.IsActive == true).Select(a => new CommentDetailWithUser
            {
                Articleid = a.ArticleId,
                Text = a.Text,
                UserId = a.AppUserId,
                username = a.AppUser.FullName,
                CreateDate = a.CreateDate,
                Image = a.AppUser.Image
            }).Where(a => a.Articleid == (int)idmakale).ToList();

            //List<CommentDetailWithUser> editcomments = commentRepository.CommentUserArticle(a => a.IsActive == true).Select(a => new CommentDetailWithUser
            //{ 
            //    Text = a.Text,
            //    UserId = a.AppUserId
            //}).Where(a => a.UserId == (int)idkullanici).ToList();

            return View(readcomments);


        }
    }
}
