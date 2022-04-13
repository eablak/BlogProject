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
    public class CommentRepository : BaseRepository<Comment>, ICommentRepository
    {

        public CommentRepository(AppDbContext appDbContext) : base(appDbContext)
        {

        }

       

        public List<Comment> CommentUserArticle(Expression<Func<Comment, bool>> expression)
        {
            return _table.Where(expression).Include(a=>a.AppUser).Include(a => a.Article).ToList();
        }

      
    }
}
