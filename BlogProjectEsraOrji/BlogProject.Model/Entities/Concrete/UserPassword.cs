using System;
using System.Collections.Generic;
using System.Text;

namespace BlogProject.Model.Entities.Concrete
{
    public class UserPassword
    {
        public int AppUserID { get; set; }

        public AppUser AppUser { get; set; }

        public int PasswordID { get; set; }
        public Password Password { get; set; }
    }
}
