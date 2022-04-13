using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BlogProject.Web.Areas.Member.Models.VMs
{
    public class GetAppUserVM
    {
        public int Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        public string Image { get; set; }

        [NotMapped] 
        public IFormFile ImagePath { get; set; }

        public string Mail { get; set; }
    }
}
