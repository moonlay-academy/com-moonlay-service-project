using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Com.Moonlay.Service.Project.Lib.Models;
using Microsoft.EntityFrameworkCore;
using Com.Moonlay.Models;

namespace Com.Moonlay.Service.Project.Lib.Services
{
    public abstract class BaseService<TModel, TKey> : IService<TModel, TKey>
        where TModel : class, IEntity
        where TKey : IConvertible
    {
        public ProjectDbContext DbContext { get; private set; }
        public DbSet<TModel> Set { get; private set; }
        public BaseService(ProjectDbContext dbContext)
        {
            this.DbContext = dbContext;
            this.Set = this.DbContext.Set<TModel>();
        }

        public IEnumerable<TModel> Get()
        {
            return this.Set;
        }

        public Task<TModel> GetAsync(TKey id)
        {
            return this.Set.SingleOrDefaultAsync(e => e.Id.Equals(id));
        }
        public TModel Get(TKey id)
        {
            return this.Set.SingleOrDefault(e => e.Id.Equals(id));
        }

        public virtual void OnUpdating(TKey id, TModel model)
        {
        }
        public Task<int> UpdateAsync(TKey id, TModel model)
        {
            try
            {
                this.DbContext.Entry(model).State = EntityState.Modified;
                this.OnUpdating(id, model);
                return this.DbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IsExists(id))
                {
                    throw;
                }
                else
                {
                    throw;
                }
            }
        }
        public int Update(TKey id, TModel model)
        {
            try
            {
                this.DbContext.Entry(model).State = EntityState.Modified;
                this.OnUpdating(id, model);
                return this.DbContext.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IsExists(id))
                {
                    throw;
                }
                else
                {
                    throw;
                }
            }
        }
        public virtual void OnCreating(TModel model)
        {
        }
        public Task<int> CreateAsync(TModel model)
        {
            this.Set.Add(model);
            this.OnCreating(model);
            return this.DbContext.SaveChangesAsync();
        }
        public int Create(TModel model)
        {
            this.Set.Add(model);
            this.OnCreating(model);
            return this.DbContext.SaveChanges();
        }

        public Task<int> DeleteAsync(TKey id)
        {
            var entity = this.Get(id);
            if (entity == null)
            {
                throw new Exception();
            }
            
            this.OnDeleting(entity);
            return this.DbContext.SaveChangesAsync();
        }
        public int Delete(TKey id)
        {
            var entity = this.Get(id);
            if (entity == null)
            {
                throw new Exception();
            }
            this.OnDeleting(entity);
            return this.DbContext.SaveChanges();
        }
        public virtual void OnDeleting(TModel entity)
        {
            this.Set.Remove(entity);
        }

        public bool IsExists(TKey id)
        {
            return this.Set.Any(m => m.Id.Equals(id));
        }



        IEnumerable<object> IService.Get()
        {
            return Get();
        }

        Task<object> IService.GetAsync(object id)
        {
            return System.Threading.Tasks.Task.FromResult((object)Get((TKey)id));
        }
        object IService.Get(object id)
        {
            return Get((TKey)id);
        }

        Task<int> IService.UpdateAsync(object id, object model)
        {
            return UpdateAsync((TKey)id, model as TModel);
        }
        int IService.Update(object id, object model)
        {
            return Update((TKey)id, model as TModel);
        }

        Task<int> IService.CreateAsync(object model)
        {
            return CreateAsync(model as TModel);
        }

        int IService.Create(object model)
        {
            return Create(model as TModel);
        }

        Task<int> IService.DeleteAsync(object id)
        {
            return DeleteAsync((TKey)id);
        }
        int IService.Delete(object id)
        {
            return Delete((TKey)id);
        }

        bool IService.IsExists(object id)
        {
            return IsExists((TKey)id);
        }
    }
}
