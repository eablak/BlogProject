using BlogProject.Model.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogProject.Model.Entities.Concrete
{
    public class ArticleCategory 
    {
        public int ArticleID { get; set; }
        public Article Article { get; set; }

        public int CategoryID { get; set; }
        public Category Category { get; set; }
    }
}
