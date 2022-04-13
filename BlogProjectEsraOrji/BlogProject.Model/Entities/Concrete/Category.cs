using BlogProject.Model.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogProject.Model.Entities.Concrete
{
    public class Category : BaseEntity
    {
        public Category()
        {
            Articles = new List<Article>();  // ileride bu sınıfın listesine nesne eklenmek istendiginde liste olmadıgı için hata verecekti ama artık bu sınıftan bır ınstance alındıgında zaten liste boşta olsa oluşmuş olacağı için hata fırlatmayacaktır.
            UserFollowedCategories = new List<UserFollowedCategory>();
            //ArticleCategories = new List<ArticleCategory>();
        }
        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }


        // navigation prop
        // 1 kategori - çok makale olabilir.

        public List<Article> Articles { get; set; } //BURAYI VE YUKARISINI YORUMDAN ÇIKARDIM

        public List<UserFollowedCategory>  UserFollowedCategories { get; set; }

        //public List<ArticleCategory> ArticleCategories { get; set; }
     

    }
}
