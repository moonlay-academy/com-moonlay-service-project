using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Com.Moonlay.Service.Project.Lib.Models;
using Microsoft.EntityFrameworkCore;

namespace Com.Moonlay.Service.Project.Lib.Services
{
    public class ProjectService : StandardEntityService<ProjectDbContext, Models.Project>
    {
        public ProjectService(IServiceProvider serviceProvider) : base(serviceProvider)
        {

        }
    }
}
