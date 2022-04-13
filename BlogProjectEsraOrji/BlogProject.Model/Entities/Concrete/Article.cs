using BlogProject.Model.Entities.Abstract;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogProject.Model.Entities.Concrete
{
    public class Article : BaseEntity
    {

        public Article()
        {
            Comments = new List<Comment>();
            Likes = new List<Like>();
            ArticleCategories=new List<ArticleCategory>();
            //ArticleCategories = new List<ArticleCategory>();
        }
        public string Title { get; set; }

        public string Content { get; set; }

        public string Image { get; set; }

        public int viewCount { get; set; }

        public bool IsActive { get; set; }


        [NotMapped]
        public IFormFile ImagePath { get; set; }
        //navigation prop
        // 1 makalenin - 1 kategorisi

        //public int CategoryId { get; set; }
        /*public Category  Category { get; set; } *///GİRİŞTE KATEGORİ GÖRNME DENEMESİ
        public ArticleCategory ArticleCategory { get; set; }

        // 1 makalenin - 1 kullnıcısı( oluşturanı )
        public int AppUserId { get; set; }

        public AppUser AppUser { get; set; }

        // 1 makalenin - çok beğenisi

        public List<Like> Likes { get; set; }

        // 1 makalenin - çok yorumu 
        public List<Comment> Comments { get; set; }

        public List<ArticleCategory> ArticleCategories { get; set; }

    }
}
