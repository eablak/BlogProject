using BlogProject.Model.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq;


namespace BlogProject.Infrastructure.Repositories.Interfaces.BaseRepository
{
    public interface IBaseRepository<T> where T: BaseEntity
    {
        void Create(T entity);

        void Update(T entity);

        void Delete(T entity);

        bool Any(Expression<Func<T,bool>> expression);

        T GetDefault(Expression<Func<T, bool>> expression);  // tek bir nesne döner
        List<T> GetDefaults(Expression<Func<T, bool>> expression);  // aynı tipte tüm nesneleri döner

        TResult GetByDefault<TResult>(Expression<Func<T, TResult>> selector,
                                      Expression<Func<T, bool>> expression,
                                      Func<IQueryable<T>, IIncludableQueryable<T,object>> include=null );

        List< TResult> GetByDefaults<TResult>(Expression<Func<T, TResult>> selector,    // seçim
                                              Expression<Func<T, bool>> expression,     // sorgu
                                              Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,  // içermesini-dahil etmesini istediğimiz sınıflar, proplar vs.
                                              Func<IQueryable<T>,IOrderedQueryable<T>>  orderby=null ); // istenen sıralama var ise


    }
}
