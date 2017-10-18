using Com.Moonlay.Service.Project.Lib;
using Com.Moonlay.Service.Project.Lib.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Com.Moonlay.Service.Project.Test
{
    public class HelperService
    {
        public HelperService(
            ProjectDbContext dbContext,
            ProjectService projectService,
            BacklogService backlogService)
        {
            this.DbContext = dbContext;
            this.ProjectService = projectService;// new ProjectService(dbContext);
            this.BacklogService = backlogService;// new BacklogService(dbContext);
        }

        ProjectDbContext DbContext { get; set; }
        ProjectService ProjectService { get; set; }
        BacklogService BacklogService { get; set; }


        public Task<Lib.Models.Project> GetTestProject()
        {

            var testProject = ProjectService.Set.FirstOrDefault(project => project.Code == "TEST");
            if (testProject != null)
                return Task.FromResult(testProject);
            else
            {
                testProject = new Lib.Models.Project()
                {
                    Code = "TEST",
                    Name = "Test Project",
                    Description = "Test Project Description"
                };
                var id = ProjectService.Create(testProject);
                return ProjectService.Set.FindAsync(id);
            }
        }
    }
}
