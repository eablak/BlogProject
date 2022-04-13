using BlogProject.Infrastructure.Repositories.Interfaces.BaseRepository;
using BlogProject.Model.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BlogProject.Infrastructure.Repositories.Interfaces.EntityTypeRepository
{
    public interface IArticleRepository : IBaseRepository<Article>
    {
        List<Article> GetArticleWithUser(Expression<Func<Article, bool>> expression);
        int ArticleTime(int id);
        //int ArticleTime(Expression<Func<Article, bool>> expression);


    }
}
