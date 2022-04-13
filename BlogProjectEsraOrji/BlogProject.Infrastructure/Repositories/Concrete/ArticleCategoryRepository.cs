using BlogProject.Infrastructure.Context;
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
    public class ArticleCategoryRepository : IArticleCategoryRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly DbSet<ArticleCategory> _table;

        public ArticleCategoryRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            _table = _appDbContext.Set<ArticleCategory>();
        }

        public List<ArticleCategory> GetDefaults(Expression<Func<ArticleCategory, bool>> expression)
        {
            return _table.Where(expression).Include(a=>a.Category).ToList();  
        }

        public List<ArticleCategory> Kategoriler(Expression<Func<ArticleCategory, bool>> expression)
        {
            return _table.Where(expression).Include(a => a.Article).ToList();
        }
    }
}
