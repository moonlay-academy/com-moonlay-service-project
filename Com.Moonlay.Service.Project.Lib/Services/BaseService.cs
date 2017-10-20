using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Com.Moonlay.Service.Project.Lib.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Com.Moonlay.Models;
using System.ComponentModel.DataAnnotations;

namespace Com.Moonlay.Service.Project.Lib.Services
{
    public abstract class BaseService : IService
    {
        public IServiceProvider ServiceProvider { get; private set; }
        public virtual DbContext DbContext { get; set; }

        public BaseService(IServiceProvider serviceProvider)
        {
            this.ServiceProvider = serviceProvider;
        }

        public abstract int Create(object model);
        public abstract Task<int> CreateAsync(object model);
        public abstract int Delete(object id);
        public abstract Task<int> DeleteAsync(object id);
        public abstract IEnumerable<object> Get();
        public abstract object Get(object id);
        public abstract Task<object> GetAsync(object id);
        public abstract bool IsExists(object id);
        public abstract int Update(object id, object model);
        public abstract Task<int> UpdateAsync(object id, object model);
    }

    public abstract class BaseService<TDbContext, TModel, TKey> : BaseService, IService<TModel, TKey>
        where TDbContext : DbContext
        where TModel : class, IEntity, IValidatableObject
        where TKey : IConvertible
    {
        TDbContext _dbContext;
        DbSet<TModel> _dbSet;

        public BaseService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override DbContext DbContext
        {
            get
            {
                if (_dbContext == null)
                    _dbContext = this.ServiceProvider.GetService<TDbContext>();
                return _dbContext;
            }
        }
        public DbSet<TModel> DbSet
        {
            get
            {
                if (_dbSet == null)
                    _dbSet = this.DbContext.Set<TModel>();
                return _dbSet;
            }
        }

        public virtual void OnCreating(TModel model)
        {
        }
        public int Create(TModel model)
        {
            this.DbSet.Add(model);
            this.OnCreating(model);

            List<ValidationResult> validationResults = new List<ValidationResult>();
            ValidationContext validationContext = new ValidationContext(model, this.ServiceProvider, null);

            if (Validator.TryValidateObject(model, validationContext, validationResults, true))
                return this.DbContext.SaveChanges();
            else
                throw new ServiceValidationExeption(validationContext, validationResults);
        }
        public Task<int> CreateAsync(TModel model)
        {
            this.DbSet.Add(model);
            this.OnCreating(model);
            List<ValidationResult> validationResults = new List<ValidationResult>();
            ValidationContext validationContext = new ValidationContext(model, this.ServiceProvider, null);

            if (Validator.TryValidateObject(model, validationContext, validationResults, true))
                return this.DbContext.SaveChangesAsync();
            else
                throw new ServiceValidationExeption(validationContext, validationResults);
        }

        public virtual void OnDeleting(TModel entity)
        {
            this.DbSet.Remove(entity);
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

        public TModel Get(TKey id)
        {
            return this.DbSet.SingleOrDefault(e => e.Id.Equals(id));
        }

        public Task<TModel> GetAsync(TKey id)
        {
            return this.DbSet.SingleOrDefaultAsync(e => e.Id.Equals(id));
        }

        public bool IsExists(TKey id)
        {
            return this.DbSet.Any(m => m.Id.Equals(id));
        }

        public virtual void OnUpdating(TKey id, TModel model)
        {
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

        IEnumerable<TModel> IService<TModel, TKey>.Get()
        {
            return this.DbSet;
        }


        public override int Create(object model)
        {
            return this.Create(model as TModel);
        }
        public override Task<int> CreateAsync(object model)
        {
            return this.CreateAsync(model as TModel);
        }
        public override int Delete(object id)
        {
            return this.Delete((TKey)id);
        }
        public override Task<int> DeleteAsync(object id)
        {
            return this.DeleteAsync((TKey)id);
        }
        public override IEnumerable<object> Get()
        {
            return this.Get();
        }
        public override object Get(object id)
        {
            return this.Get((TKey)id);
        }
        public override Task<object> GetAsync(object id)
        {
            return System.Threading.Tasks.Task.FromResult((object)Get((TKey)id));
        }
        public override bool IsExists(object id)
        {
            return this.IsExists((TKey)id);
        }
        public override int Update(object id, object model)
        {
            return this.Update((TKey)id, model as TModel);
        }
        public override Task<int> UpdateAsync(object id, object model)
        {
            return this.UpdateAsync((TKey)id, model as TModel);
        }

        //override IEnumerable<object> IService.Get()
        //{
        //}

        //Task<object> IService.GetAsync(object id)
        //{
        //    return System.Threading.Tasks.Task.FromResult((object)Get((TKey)id));
        //}
        //object IService.Get(object id)
        //{
        //    return Get((TKey)id);
        //}

        //Task<int> IService.UpdateAsync(object id, object model)
        //{
        //    return UpdateAsync((TKey)id, model as TModel);
        //}
        //int IService.Update(object id, object model)
        //{
        //    return Update((TKey)id, model as TModel);
        //}

        //Task<int> IService.CreateAsync(object model)
        //{
        //    return CreateAsync(model as TModel);
        //}

        //int IService.Create(object model)
        //{
        //    return Create(model as TModel);
        //}

        //Task<int> IService.DeleteAsync(object id)
        //{
        //    return DeleteAsync((TKey)id);
        //}
        //int IService.Delete(object id)
        //{
        //    return Delete((TKey)id);
        //}

        //bool IService.IsExists(object id)
        //{
        //    return IsExists((TKey)id);
        //} 
    }

    public class ServiceValidationExeption : Exception
    {
        public ServiceValidationExeption(ValidationContext validationContext, IEnumerable<ValidationResult> validationResults) : this("Validation Error", validationContext, validationResults)
        {

        }
        public ServiceValidationExeption(string message, ValidationContext validationContext, IEnumerable<ValidationResult> validationResults) : base(message)
        {
            this.ValidationContext = validationContext;
            this.ValidationResults = validationResults;
        }

        public ValidationContext ValidationContext { get; private set; }
        public IEnumerable<ValidationResult> ValidationResults { get; private set; }
    }
}
