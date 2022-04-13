using BlogProject.Infrastructure.Repositories.Interfaces.EntityTypeRepository;
using BlogProject.Web.Areas.Member.Models.ViewEntity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace BlogProject.Web.Views.Shared.Components.Comments
{
    [ViewComponent(Name = "Comments")]
    public class CommentViewComponent : ViewComponent
    {
        private readonly ICommentRepository commentRepository;

        public CommentViewComponent(ICommentRepository commentRepository)
        {
            this.commentRepository = commentRepository;
        }

        public IViewComponentResult Invoke()
        {

            //List<UserComment> comments = commentRepository.GetCommentWithUser(a => a.Statu != Model.Enums.Statu.Passive).Select(a => new UserComment
            //{
            //    UserId = a.Id,
            //    Text = a.Text
            //}).ToList();

            return View(/*comments*/);
        }
    }
}
