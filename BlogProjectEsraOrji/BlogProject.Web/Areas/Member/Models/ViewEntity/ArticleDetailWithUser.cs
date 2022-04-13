using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BlogProject.Web.Areas.Member.Models.ViewEntity
{
    public class ArticleDetailWithUser
    {
        public int UserId { get; set; }

        public string UserFullName { get; set; }

        public string Image { get; set; }

        [NotMapped]
        public IFormFile ImagePath { get; set; }


        public int ArticleId { get; set; }
        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime CreateDate { get; set; }

    }
}
