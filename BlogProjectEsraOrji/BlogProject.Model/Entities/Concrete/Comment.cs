using BlogProject.Model.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogProject.Model.Entities.Concrete
{
    public class Comment : BaseEntity
    {
        public string Text { get; set; }
        public bool IsActive { get; set; }


        // navigation prop
        // 1 yorum - 1 kullanıcıya ait

        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        // 1 yorum - 1 makaleye

        public int ArticleId { get; set; }

        public Article  Article { get; set; }

    }
}
