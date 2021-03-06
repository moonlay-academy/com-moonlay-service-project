using Com.Moonlay.Models;
using Com.Moonlay.Service.Project.Lib;
using Com.Moonlay.Service.Project.Lib.Models;
using Com.Moonlay.Service.Project.Lib.Services;
using IdentityModel.Client;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Com.Moonlay.Service.Project.Test.Services
{
    [Collection("ServiceProviderFixture collection")]
    public abstract class ServiceBasicCRUDTest<TService, TModel> : IDisposable
        where TService : BaseService
        where TModel : StandardEntity, IValidatableObject, new()
    {
        IServiceProvider serviceProvider;
        public ServiceBasicCRUDTest(ServiceProviderFixture fixture)
        {
            this.serviceProvider = fixture.ServiceProvider;
        }
        protected TService Service
        {
            get
            {
                return this.serviceProvider.GetService<TService>();
            }
        }
        protected ProjectDbContext DbContext
        {
            get
            {
                return this.serviceProvider.GetService<ProjectDbContext>();
            }
        }
        protected HelperService Helper
        {
            get
            {
                return this.serviceProvider.GetService<HelperService>();
            }
        }


        public virtual TModel GetCreateTestModel()
        {
            return this.GenerateTestModel();
        }
        public abstract TModel GenerateTestModel();


        [Fact]
        public async System.Threading.Tasks.Task TestCreateAsync()
        {
            var service = this.serviceProvider.GetService<TService>();
            var testData = GetCreateTestModel();
            var createdCount = await service.CreateAsync(testData);
            var id = testData.Id;
            Assert.True(createdCount == 1);

            var data = service.DbContext.Set<TModel>().FindAsync(id);
            Assert.NotNull(data);
        }
        [Fact]
        public void TestCreate()
        {
            var service = this.serviceProvider.GetService<TService>();
            var testData = GetCreateTestModel();
            var createdCount = service.Create(testData);
            var id = testData.Id;
            Assert.True(createdCount == 1);

            var data = service.DbContext.Set<TModel>().Find(id);
            Assert.NotNull(data);
        }

        [Fact]
        public async System.Threading.Tasks.Task TestUpdateAsync()
        {
            var service = this.serviceProvider.GetService<TService>();
            var testData = GetCreateTestModel();

            var createdCount = await service.CreateAsync(testData);
            var id = testData.Id;
            var data = await service.DbContext.Set<TModel>().FindAsync(id);
            Assert.NotNull(data);
            Assert.True(createdCount == 1);

            var updatedCount = await service.UpdateAsync(data.Id, data);
            Assert.True(updatedCount == 1);
        }

        [Fact]
        public void TestUpdate()
        {
            var service = this.serviceProvider.GetService<TService>();
            var testData = GetCreateTestModel();

            var createdCount = service.Create(testData);
            var id = testData.Id;
            var data = service.DbContext.Set<TModel>().Find(id);
            Assert.NotNull(data);
            Assert.True(createdCount == 1);

            var updatedCount = service.Update(data.Id, data);
            Assert.True(updatedCount == 1);
        }

        [Fact]
        public async System.Threading.Tasks.Task TestDeleteAsync()
        {
            var service = this.serviceProvider.GetService<TService>();
            var testData = GetCreateTestModel();

            var affectedCount = await service.CreateAsync(testData);
            var id = testData.Id;
            var data = await service.DbContext.Set<TModel>().FindAsync(id);

            Assert.NotNull(data);
            Assert.True(affectedCount == 1);

            var affectedResult = await service.DeleteAsync(data.Id);
            Assert.True(affectedResult == 1);

            data = await service.DbContext.Set<TModel>().FirstOrDefaultAsync(m => m.Id == id);
            Assert.Null(data);

            data = await service.DbContext.Set<TModel>().FindAsync(id);
            Assert.NotNull(data);
            Assert.True(data._IsDeleted);

        }


        public void Dispose()
        {
            this.serviceProvider = null;
        }
    }
}
