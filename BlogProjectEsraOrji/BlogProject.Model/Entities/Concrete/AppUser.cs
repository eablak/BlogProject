using BlogProject.Model.Entities.Abstract;
using BlogProject.Model.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BlogProject.Model.Entities.Concrete
{
    public class AppUser : BaseEntity
    {

        public AppUser()
        {
            Articles = new List<Article>();
            Comments = new List<Comment>();
            Likes = new List<Like>();
            UserFollowedCategories = new List<UserFollowedCategory>(); // çoka - çok ilişki
            Passwords = new HashSet<Password>();
            
        }

        public string IdentityId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string Mail { get; set; }

        public bool IsActive { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        public string Image { get; set; }  // fotograflar için

        [NotMapped]  // configure ederken veritabanı tarafında kolon olarak oluşturma.
        public IFormFile ImagePath { get; set; }

        public Role Role { get; set; }

        public ICollection<Password> Passwords { get; set; }


        // navigation prop
        // 1 kullanıcı - çok makale
        public List<Article> Articles { get; set; }

        // 1 kullanıcı -  çok yorum

        public List<Comment>  Comments { get; set; }

        // 1 kullanıcı - çok beğeni

        public List<Like> Likes { get; set; }

        public List<UserFollowedCategory>  UserFollowedCategories { get; set; }


        public List<UserPassword> UserPasswords { get; set; }
    }
}
