using BlogProject.Model.Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogProject.Web.Areas.Member.Controllers
{
    [Area("Member")]
    public class MemberController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
