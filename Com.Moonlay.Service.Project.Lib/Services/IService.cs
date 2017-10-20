using Com.Moonlay.Models;
using Com.Moonlay.Service.Project.Lib.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Moonlay.Service.Project.Lib.Services
{
    public interface IService
    {

        IEnumerable<object> Get();
        Task<object> GetAsync(object id);
        object Get(object id);

        Task<int> CreateAsync(object model);
        int Create(object model);

        Task<int> UpdateAsync(object id, object model);
        int Update(object id, object model);

        Task<int> DeleteAsync(object id);
        int Delete(object id);
        bool IsExists(object id);
    }

    public interface IService<TModel, TKey> : IService
        where TModel : IEntity
        where TKey : IConvertible
    {
        new IEnumerable<TModel> Get();
        Task<TModel> GetAsync(TKey id);
        TModel Get(TKey id);

        Task<int> UpdateAsync(TKey id, TModel model);
        int Update(TKey id, TModel model);

        Task<int> CreateAsync(TModel model);
        int Create(TModel model);

        Task<int> DeleteAsync(TKey id);
        int Delete(TKey id);
        bool IsExists(TKey id);
    }
}
