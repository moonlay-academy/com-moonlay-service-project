using Com.Moonlay.Service.Project.Lib;
using Com.Moonlay.Service.Project.Lib.Models;
using Com.Moonlay.Service.Project.Lib.Services;
using IdentityModel.Client;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Com.Moonlay.Service.Project.Test.Services.BacklogService
{
    [Collection("ServiceProviderFixture collection")]
    public class BacklogServiceBasicCRUDTest : ServiceBasicCRUDTest<Lib.Services.BacklogService, Lib.Models.Backlog>
    {
        public BacklogServiceBasicCRUDTest(ServiceProviderFixture fixture) : base(fixture)
        {
        }

        public override Lib.Models.Backlog GenerateTestModel()
        {
            var testProject = this.Helper.GetTestProject().Result;
            var guid = Guid.NewGuid().ToString();
            return new Lib.Models.Backlog()
            {
                Code = guid,
                ProjectId = testProject.Id,
                Name = "TEST BACKLOG - 01",
                Description = "TEST BACKLOG DESCRIPTION"
            };
        }
    }
}
