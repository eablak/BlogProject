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
    public class ArticleRepository : BaseRepository<Article>, IArticleRepository
    {

        public ArticleRepository(AppDbContext appDbContext) : base(appDbContext)
        {

        }

        public List<Article> GetArticleWithUser(Expression<Func<Article, bool>> expression)
        {
            return _table.Where(expression).Include(a=>a.ArticleCategory).Include(a => a.AppUser).ToList();
        }

        public int ArticleTime(int id)
        {
            List<string> metin = _appDbContext.Articles.Where(a => a.Id == id).Select(a => a.Content).ToList();
            int karaktersayisi = metin.FirstOrDefault().Length;
            return karaktersayisi / 50;
        }
    }
}
