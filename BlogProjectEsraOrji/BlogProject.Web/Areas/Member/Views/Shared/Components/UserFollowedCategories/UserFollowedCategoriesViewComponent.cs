using BlogProject.Infrastructure.Repositories.Interfaces.EntityTypeRepository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogProject.Web.Areas.Member.Views.Shared.Components.UserFollowedCategories
{
    [ViewComponent(Name = "UserFollowedCategories")]
    public class UserFollowedCategoriesViewComponent :ViewComponent
    {
        private readonly ICategoryRepository categoryRepository;

        public UserFollowedCategoriesViewComponent(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }


        public IViewComponentResult Invoke(int? id)
        {
            var folowedList = categoryRepository.GetCategoriesWithUser((int)id);
            return View(folowedList);
        }



    }
}
