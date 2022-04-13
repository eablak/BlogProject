using System.ComponentModel.DataAnnotations;

namespace BlogProject.Web.Areas.Member.Models.ViewEntity
{
    public class UserCommentVM
    {
        public int UserId { get; set; }

        public string Text { get; set; }

        public int ArticleID { get; set; }
    }
}
