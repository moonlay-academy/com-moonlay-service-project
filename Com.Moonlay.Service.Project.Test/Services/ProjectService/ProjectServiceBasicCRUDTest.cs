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
using System.Linq;

namespace Com.Moonlay.Service.Project.Test.Services.ProjectService
{
    [Collection("ServiceProviderFixture collection")]
    public class ProjectServiceBasicCRUDTest : ServiceBasicCRUDTest<Lib.Services.ProjectService, Lib.Models.Project>
    {
        public ProjectServiceBasicCRUDTest(ServiceProviderFixture fixture) : base(fixture)
        {
        }

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
        [Fact]
        public void TestCreateValidation()
        {
            var service = this.Service;
            var testData = GetCreateTestModel();
            testData.Code = string.Empty;
            //testData.Name = string.Empty;
            //testData.Description = "".PadLeft(256, '$');
            try
            {

                var createdCount = service.Create(testData);
                throw new Exception();
            }
            catch (ServiceValidationExeption ex)
            {
                var codeResult = ex.ValidationResults.FirstOrDefault(r => r.MemberNames.Contains("Code"));
                Assert.NotNull(codeResult);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
