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
    public class PasswordRepository : BaseRepository<Password>, IPasswordRepository
    {

        public PasswordRepository(AppDbContext appDbContext) : base(appDbContext)
        {

        }

        public List<Password> GetPasswordWithUser(Expression<Func<Password, bool>> expression)
        {
            return _table.Where(expression).Include(a => a.AppUser).ToList();
        }

        public List<string> PasswordText(int id)
        {

            var mesela = _appDbContext.UserPasswords.Include(a => a.AppUser).Include(a=> a.Password).Where(a => a.AppUserID == id).OrderByDescending(a => a.Password.CreateDate).Select(a => a.Password.Text).Take(3).ToList();
            return mesela;
        }

    }
}
