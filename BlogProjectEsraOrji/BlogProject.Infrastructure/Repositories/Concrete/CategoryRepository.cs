using BlogProject.Infrastructure.Context;
using BlogProject.Infrastructure.Repositories.Abstract;
using BlogProject.Infrastructure.Repositories.Interfaces.EntityTypeRepository;
using BlogProject.Model.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlogProject.Infrastructure.Repositories.Concrete
{
  public  class CategoryRepository :BaseRepository<Category>,ICategoryRepository
    {
        public CategoryRepository(AppDbContext appDbContext):base(appDbContext)
        {

        }

        public List<Category> GetCategoriesWithUser(int id)
        {
            return _appDbContext.UserFollowedCategories.Include(a => a.AppUser).Include(a => a.Category).Where(a => a.AppUserID == id).Select(a => a.Category).ToList();
        }

        public bool CategoryUnFollowed(int userId,Category category)
        {
            _appDbContext.UserFollowedCategories.Remove(new UserFollowedCategory() { AppUserID = userId, CategoryID = category.Id });
            return _appDbContext.SaveChanges() > 0;
        }
    }
}
