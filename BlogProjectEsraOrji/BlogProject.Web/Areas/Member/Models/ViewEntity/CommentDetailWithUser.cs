using BlogProject.Model.Entities.Concrete;
using System;

namespace BlogProject.Web.Areas.Member.Models.ViewEntity
{
    public class CommentDetailWithUser
    {
        public int UserId { get; set; }
        public string Text { get; set; }
        public string Image { get; set; }
        public DateTime CreateDate { get; set; }
        public int Articleid { get; set; }
        public string username { get; set; }

    }
}
