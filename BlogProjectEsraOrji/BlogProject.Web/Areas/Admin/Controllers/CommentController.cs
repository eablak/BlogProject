using BlogProject.Infrastructure.Repositories.Concrete;
using BlogProject.Infrastructure.Repositories.Interfaces.EntityTypeRepository;
using BlogProject.Model.Entities.Concrete;
using BlogProject.Web.Areas.Member.Models.ViewEntity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace BlogProject.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CommentController : Controller
    {
        private readonly ICommentRepository commentRepository;

        public CommentController(ICommentRepository  commentRepository)
        {
            this.commentRepository = commentRepository;
        }

        public IActionResult List()
        {

            List<Comment> list = commentRepository.GetDefaults(a => a.IsActive == true);

            return View(list);

            //List<CommentUser> comments = commentRepository.CommentUserArticle(a => a.IsActive == true).Select(a => new CommentUser
            //{
            //    Text = a.Text,
            //    Writer = a.AppUser.UserName
            //}).ToList();
            //return View(comments);

        }

        public IActionResult Passive(int id) 
        { 
            Comment comment = commentRepository.GetDefault(a=>a.Id == id);
            comment.IsActive = false;
            commentRepository.Update(comment);
            return RedirectToAction("List");
        }
    }
}
