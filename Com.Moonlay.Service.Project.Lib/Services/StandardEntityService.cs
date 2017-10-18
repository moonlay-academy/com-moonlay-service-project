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
    public class StandardEntityService<TModel> : BaseService<TModel, int>
        where TModel : StandardEntity
    {
        public StandardEntityService(ProjectDbContext dbContext) : base(dbContext)
        {

        }
        public override void OnCreating(TModel model)
        {
            var nowUtc = DateTime.UtcNow;
            var agent = string.Empty;
            var actor = string.Empty;

            model._CreatedBy = model._LastModifiedBy = actor;
            model._CreatedAgent = model._LastModifiedAgent = agent;
            model._CreatedUtc = model._LastModifiedUtc = nowUtc;
        }

        public override void OnUpdating(int id, TModel model)
        {
            var nowUtc = DateTime.UtcNow;
            var agent = string.Empty;
            var actor = string.Empty;

            model._LastModifiedBy = actor;
            model._LastModifiedAgent = agent;
            model._LastModifiedUtc = nowUtc;
        }

        public override void OnDeleting(TModel model)
        {
            var nowUtc = DateTime.UtcNow;
            var agent = string.Empty;
            var actor = string.Empty;

            model._IsDeleted = true;
            model._DeletedBy = actor;
            model._DeletedAgent = agent;
            model._DeletedUtc = nowUtc;
        }
    }
}
