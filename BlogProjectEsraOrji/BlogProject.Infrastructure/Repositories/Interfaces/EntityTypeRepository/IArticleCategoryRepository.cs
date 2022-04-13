using BlogProject.Infrastructure.Repositories.Interfaces.BaseRepository;
using BlogProject.Model.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BlogProject.Infrastructure.Repositories.Interfaces.EntityTypeRepository
{
    public interface IArticleCategoryRepository 
    {
        List<ArticleCategory> GetDefaults(Expression<Func<ArticleCategory, bool>> expression);
        List<ArticleCategory> Kategoriler(Expression<Func<ArticleCategory, bool>> expression);
    }
}
