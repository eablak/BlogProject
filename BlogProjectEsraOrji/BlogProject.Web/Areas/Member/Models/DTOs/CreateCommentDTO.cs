using BlogProject.Web.Areas.Member.Models.ViewEntity;
using System.Collections.Generic;

namespace BlogProject.Web.Areas.Member.Models.DTOs
{
    public class CreateCommentDTO
    {
        public string Text { get; set; }
        public int AppUserId { get; set; }
        public int ArticleId { get; set; }

        public bool IsActive { get; set; }
    }
}
