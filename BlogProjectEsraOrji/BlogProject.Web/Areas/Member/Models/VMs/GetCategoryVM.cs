using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogProject.Web.Areas.Member.Models.VMs
{
    public class GetCategoryVM
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }      

    }
}
