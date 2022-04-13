using BlogProject.Infrastructure.Repositories.Interfaces.BaseRepository;
using BlogProject.Model.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BlogProject.Infrastructure.Repositories.Interfaces.EntityTypeRepository
{
   public interface ILikeRepository 
    {
        void Create(Like entity);

        void Delete(Like entity);

        bool Any(Expression<Func<Like, bool>> expression);
        List<Like> ListCount(Expression<Func<Like, bool>> expression);
    }
}
