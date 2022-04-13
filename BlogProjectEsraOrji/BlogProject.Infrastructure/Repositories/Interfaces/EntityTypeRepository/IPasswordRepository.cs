using BlogProject.Infrastructure.Repositories.Interfaces.BaseRepository;
using BlogProject.Model.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BlogProject.Infrastructure.Repositories.Interfaces.EntityTypeRepository
{
    public interface IPasswordRepository : IBaseRepository<Password>
    {       

        List<Password> GetPasswordWithUser(Expression<Func<Password, bool>> expression);
        List<string> PasswordText(int id);
    }
}
