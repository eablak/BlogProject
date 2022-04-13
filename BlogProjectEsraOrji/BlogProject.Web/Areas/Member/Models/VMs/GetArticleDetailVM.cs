using BlogProject.Model.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogProject.Web.Areas.Member.Models.VMs
{
    public class GetArticleDetailVM
    {
        public int ArticleId { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }
        public string Image { get; set; }

        public DateTime? CreateDate { get; set; }
        public string AuthorName { get; set; }

        public string AuthorImage { get; set; }

        public string Categories { get; set; }

        public string Text { get; set; }
        public int AppUserId { get; set; }

        public int View { get; set; }

        public int minute { get; set; }
    }
}
