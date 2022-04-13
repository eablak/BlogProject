using BlogProject.Model.Entities.Concrete;
using System.Collections;
using System.Collections.Generic;

namespace BlogProject.Web.Models.VMs
{
    public class ArticleComment
    {
        public IEnumerable<Article> article { get; set; }
        public IEnumerable<Comment> comment { get; set; }
    }
}
