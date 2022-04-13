using BlogProject.Infrastructure.Context;
using BlogProject.Infrastructure.Repositories.Abstract;
using BlogProject.Infrastructure.Repositories.Interfaces.EntityTypeRepository;
using BlogProject.Model.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace BlogProject.Infrastructure.Repositories.Concrete
{
    public class LikeRepository : ILikeRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly DbSet<Like> _table;
        public LikeRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            _table = _appDbContext.Set<Like>();

        }

        public bool Any(Expression<Func<Like, bool>> expression)
        {
            return _table.Any(expression);
        }

        public void Create(Like entity)
        {
            _table.Add(entity);
            _appDbContext.SaveChanges();
        }

        public void Delete(Like entity)
        {
            _appDbContext.Entry<Like>(entity).State = EntityState.Deleted;
            _appDbContext.SaveChanges();
        }

        public List<Like> ListCount(Expression<Func<Like, bool>> expression)
        {
            return _table.Where(expression).Include(a=>a.AppUser).ToList();
        }
    }
}
