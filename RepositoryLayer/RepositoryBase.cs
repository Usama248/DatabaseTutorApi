using CommonLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        internal readonly DbContext _context;
        internal readonly IServiceProvider _serviceProvider;

        public RepositoryBase(DbContext context, IServiceProvider serviceProvider)
        {
            _context = context;
            _serviceProvider = serviceProvider;
        }

        public virtual IQueryable<TEntity> Get()
        {
            return _context.Set<TEntity>().AsQueryable();
        }

        public virtual TEntity GetById<T>(T id)
        {
            return _context.Set<TEntity>().Find(id);
        }

        public virtual IEnumerable<TEntity> GetWithCondition(Expression<Func<TEntity, bool>> expression)
        {
            return _context.Set<TEntity>().Where(expression);
        }

        public virtual async Task Post(TEntity entity, bool saveChanges = false)
        {
            SetCreateAnalysisValue(entity);
            await _context.Set<TEntity>().AddAsync(entity);
            if (saveChanges)
                await _context.SaveChangesAsync();
        }

        public virtual async Task AddRange(List<TEntity> entity)
        {
            foreach (var item in entity)
                SetCreateAnalysisValue(item);
            await _context.Set<TEntity>().AddRangeAsync(entity);
        }

        public virtual void UpdateRange(List<TEntity> entity)
        {
            foreach (var item in entity)
                SetUpdateAnalysisValue(item, false);
            _context.Set<TEntity>().UpdateRange(entity);
        }

        public virtual void Put(TEntity entity, bool saveChanges = false)
        {
            SetUpdateAnalysisValue(entity, false);
            _context.Set<TEntity>().Update(entity);
            if (saveChanges)
                _context.SaveChanges();
        }

        public virtual void SoftDelete<T>(T id, bool saveChanges = false)
        {
            var entity = GetById(id);
            SetUpdateAnalysisValue(entity, true);
            _context.Set<TEntity>().Update(entity);
            if (saveChanges)
                _context.SaveChanges();
        }

        public virtual void SoftDelete(TEntity entity, bool saveChanges = false)
        {
            SetUpdateAnalysisValue(entity, true);
            _context.Set<TEntity>().Update(entity);
            if (saveChanges)
                _context.SaveChanges();
        }

        public virtual void Delete<T>(T id, bool saveChanges = false)
        {
            _context.Set<TEntity>().Remove(GetById(id));
            if (saveChanges)
                _context.SaveChanges();
        }

        public virtual void DeleteRange<T>(IEnumerable<T> entityIDs)
        {
            foreach (var id in entityIDs)
                _context.Set<TEntity>().Remove(GetById(id));
        }

        public bool Any<T>(T id)
        {
            return GetById(id) != null;
        }

        private void SetCreateAnalysisValue(TEntity entity)
        {
            var entityProperties = entity.GetType().GetProperties();
            var property = entityProperties.FirstOrDefault(x => x.Name.ToLower() == "createdby");
            if (property != null)
                property.SetValue(entity, Utils.GetUserId(_serviceProvider));

            property = entityProperties.FirstOrDefault(x => x.Name.ToLower() == "createddate");
            if (property != null)
                property.SetValue(entity, DateTime.UtcNow);
        }

        private void SetUpdateAnalysisValue(TEntity entity, bool isSoftDelete)
        {
            var entityProperties = entity.GetType().GetProperties();
            var property = entityProperties.FirstOrDefault(x => x.Name.ToLower() == "modifiedby");
            if (property != null)
                property.SetValue(entity, Utils.GetUserId(_serviceProvider));

            property = entityProperties.FirstOrDefault(x => x.Name.ToLower() == "modifieddate");
            if (property != null)
                property.SetValue(entity, DateTime.UtcNow);

            if (isSoftDelete)
            {
                property = entityProperties.FirstOrDefault(x => x.Name.ToLower() == "isdeleted");
                if (property != null)
                    property.SetValue(entity, true);
            }
        }
    }
}
