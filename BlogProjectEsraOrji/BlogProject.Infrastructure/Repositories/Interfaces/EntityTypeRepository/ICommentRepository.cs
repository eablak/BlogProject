using BlogProject.Infrastructure.Repositories.Interfaces.BaseRepository;
using BlogProject.Model.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BlogProject.Infrastructure.Repositories.Interfaces.EntityTypeRepository
{
    public interface ICommentRepository : IBaseRepository<Comment>
    {

        List<Comment> CommentUserArticle(Expression<Func<Comment, bool>> expression);

    }
}
