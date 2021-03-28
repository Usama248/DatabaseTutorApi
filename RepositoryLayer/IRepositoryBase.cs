using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RepositoryLayer
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        IQueryable<TEntity> Get();
        TEntity GetById<T>(T id);
        IEnumerable<TEntity> GetWithCondition(Expression<Func<TEntity, bool>> expression);
        Task Post(TEntity entity, bool saveChanges = false);
        Task AddRange(List<TEntity> entity);
        void Put(TEntity entity, bool saveChanges = false);
        void UpdateRange(List<TEntity> entity);
        void Delete<T>(T id, bool saveChanges = false);
        void DeleteRange<T>(IEnumerable<T> entityIDs);
        bool Any<T>(T id);
        void SoftDelete<T>(T id, bool saveChanges = false);
        void SoftDelete(TEntity entity, bool saveChanges = false);
    }
}
