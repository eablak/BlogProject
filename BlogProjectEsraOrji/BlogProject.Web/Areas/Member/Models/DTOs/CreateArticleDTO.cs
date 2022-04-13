using BlogProject.Web.Areas.Member.Models.ViewEntity;
using BlogProject.Web.Areas.Member.Models.VMs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BlogProject.Web.Areas.Member.Models.DTOs
{
    public class CreateArticleDTO
    {
        [Required(ErrorMessage = "baslık boş bırakılamaz")]
        [MinLength(3, ErrorMessage = "en az 3 karakter yazmalısınız")]
        public string Title { get; set; }

        [Required(ErrorMessage = "icerik boş bırakılamaz")]
        [MinLength(50, ErrorMessage = "en az 50 karakter yazmalısınız")]
        public string Content { get; set; }

        public string Image { get; set; }

        [NotMapped]
        public IFormFile ImagePath { get; set; }

        //[Required]
        //public int CategoryId { get; set; }

        public int AppUserId { get; set; }

        public List<GetCategoryVM> Categories { get; set; }

        public List<UserCommentVM> Comments { get; set; }

    }
}
