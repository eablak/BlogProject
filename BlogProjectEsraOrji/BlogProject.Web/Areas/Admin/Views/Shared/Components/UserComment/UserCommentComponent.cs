using BlogProject.Infrastructure.Repositories.Interfaces.EntityTypeRepository;
using BlogProject.Web.Areas.Admin.Models.ViewEntity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace BlogProject.Web.Areas.Admin.Views.Shared.Components.Comments
{
    [ViewComponent(Name = "UserComment")]
    public class UserCommentComponent : ViewComponent
    {
        private readonly ICommentRepository commentRepository;

        public UserCommentComponent(ICommentRepository commentRepository)
        {
            this.commentRepository = commentRepository;
        }

        public IViewComponentResult Invoke()
        {
            List<CommentUser> comments = commentRepository.CommentUserArticle(a => a.IsActive == true).Select(a => new CommentUser
            {
                Text = a.Text,
                Writer = a.AppUser.FullName,
                Id = a.Id
            }).ToList();

            return View(comments);
        }
    }
}
