using BlogProject.Model.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogProject.Model.Entities.Concrete
{
    public class Password : BaseEntity
    {
        public Password()
        {
            UserPasswords = new List<UserPassword>();
        }
        public string Text { get; set; }

        // nav. prop

        public int UserId { get; set; }

        public AppUser AppUser { get; set; }

        public List<UserPassword> UserPasswords { get; set; }
    }
}
