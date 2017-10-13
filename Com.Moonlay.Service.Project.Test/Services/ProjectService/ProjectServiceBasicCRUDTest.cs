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

namespace Com.Moonlay.Service.Project.Test.Services.ProjectService
{
    public class ProjectServiceBasicCRUDTest : ServiceBasicCRUDTest<Lib.Services.ProjectService, Lib.Models.Project>
    {
        public override Lib.Models.Project GenerateTestModel()
        {
            var guid = Guid.NewGuid().ToString();
            return new Lib.Models.Project()
            {
                Code = guid,
                Name = string.Format("TEST PROJECT {0}", guid),
                Description = "TEST PROJECT DESCRIPTION"
            };
        }
    }
}
