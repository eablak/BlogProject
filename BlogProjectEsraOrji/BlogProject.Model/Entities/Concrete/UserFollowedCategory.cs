using System;
using System.Collections.Generic;
using System.Text;

namespace BlogProject.Model.Entities.Concrete
{
    public class UserFollowedCategory
    {
        //COMPOSİTE KEY 
        public int AppUserID { get; set; }

        public AppUser  AppUser { get; set; }


        public int CategoryID { get; set; }

        public Category  Category { get; set; }
    }
}
