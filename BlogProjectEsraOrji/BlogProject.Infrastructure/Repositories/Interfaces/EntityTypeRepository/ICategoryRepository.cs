using BlogProject.Infrastructure.Repositories.Interfaces.BaseRepository;
using BlogProject.Model.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogProject.Infrastructure.Repositories.Interfaces.EntityTypeRepository
{
   public interface ICategoryRepository : IBaseRepository<Category>
    {
        bool CategoryUnFollowed(int userId, Category category);
        List<Category> GetCategoriesWithUser(int id);
    }
}
