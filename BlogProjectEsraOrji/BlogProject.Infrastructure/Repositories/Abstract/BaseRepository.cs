using BlogProject.Infrastructure.Context;
using BlogProject.Infrastructure.Repositories.Interfaces.BaseRepository;
using BlogProject.Model.Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace BlogProject.Infrastructure.Repositories.Abstract
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly AppDbContext _appDbContext;

        protected readonly DbSet<T> _table;

        public BaseRepository(AppDbContext appDbContext)
        {
           _appDbContext = appDbContext;
            _table = _appDbContext.Set<T>();
        }

        public void Create(T entity)
        {
            _table.Add(entity);
            _appDbContext.SaveChanges();
        }

        public void Update(T entity)
        {
            _appDbContext.Entry<T>(entity).State = EntityState.Modified;  // gelen verinin durumunu değiştimesi gerektiğini yazdık.
            _appDbContext.SaveChanges();  // değişiklikleri kaydet

        }

        public void Delete(T entity)
        {
            _appDbContext.Entry<T>(entity).State = EntityState.Deleted;
            _appDbContext.SaveChanges();
        }

        public bool Any(Expression<Func<T, bool>> expression)
        {
            return _table.Any(expression); //linQ sorgusu - var mı-isimle aynı olmak zorunda değil,tesadüf.
        }

        public T GetDefault(Expression<Func<T, bool>> expression)
        {
            return _table.FirstOrDefault(expression);  // linq sorguus - tabloda sorguya uyn ilk degeri döndür
        }

        public List<T> GetDefaults(Expression<Func<T, bool>> expression)
        {
           return  _table.Where(expression).ToList();  // sorguya uyan her T tipini list yap,getir.
        }

        public TResult GetByDefault<TResult>(Expression<Func<T, TResult>> selector, 
                                             Expression<Func<T, bool>> expression,
                                              Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            IQueryable<T> query = _table;  //sorguların gerçekleşeceği yer T tipne ait tablonun ta kendisi

            if (include!=null)  // include sorusu boş değilse yani paramete geldiyse ona ait işlemleri yap!
            {
                query = include(query);
            }
            if (expression!=null) // expression sorgusu da verildiyse ona ait sorgularda yap!
            {
                query = query.Where(expression);
            }

            return query.Select(selector).FirstOrDefault(); // include ve expression sorgusu dolu/boş gelsede en sonda bu seçim işlemi yapılacaktır.
           
        }

        public List<TResult> GetByDefaults<TResult>(Expression<Func<T, TResult>> selector,
                                                    Expression<Func<T, bool>> expression,
                                                    Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
                                                    Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null)
        {
            IQueryable<T> query = _table;  //sorguların gerçekleşeceği yer T tipne ait tablonun ta kendisi

            if (include != null)  // include sorusu boş değilse yani paramete geldiyse ona ait işlemleri yap!
            {
                query = include(query);
            }
            if (expression != null) // expression sorgusu da verildiyse ona ait sorgularda yap!
            {
                query = query.Where(expression);
            }
            if (orderby!=null)
            {
                return orderby(query).Select(selector).ToList();
            }
            else
            {
                return query.Select(selector).ToList();
            }
        }
    }
}
