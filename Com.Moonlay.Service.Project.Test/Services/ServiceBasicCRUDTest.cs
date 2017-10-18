using Com.Moonlay.Models;
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

namespace Com.Moonlay.Service.Project.Test.Services
{
    public abstract class ServiceBasicCRUDTest<TService, TModel> : IDisposable
        where TService : StandardEntityService<TModel>
        where TModel : StandardEntity, new()
    {
        IServiceProvider serviceProvider;
        public ServiceBasicCRUDTest()
        {
            var connectionString = @"Server=tcp:127.0.0.1,1401;Database=com.moonlay.db.project;User=sa;password=Standar123.;MultipleActiveResultSets=true;Persist Security Info=True";
            this.serviceProvider = new ServiceCollection()
                .AddDbContext<ProjectDbContext>(options => options.UseSqlServer(connectionString))
                .AddSingleton<TService>()
                .AddSingleton<HelperService>()
                .BuildServiceProvider();
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

            var data = service.Set.FindAsync(id);
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

            var data = service.Set.Find(id);
            Assert.NotNull(data);
        }

        [Fact]
        public async System.Threading.Tasks.Task TestUpdateAsync()
        {
            var service = this.serviceProvider.GetService<TService>();
            var testData = GetCreateTestModel();

            var createdCount = await service.CreateAsync(testData);
            var id = testData.Id;
            var data = await service.Set.FindAsync(id);
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
            var data = service.Set.Find(id);
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
            var data = await service.Set.FindAsync(id);

            Assert.NotNull(data);
            Assert.True(affectedCount == 1);

            var affectedResult = await service.DeleteAsync(data.Id);
            Assert.True(affectedResult == 1);

            data = await service.Set.FindAsync(id);
            Assert.Null(data);
        }


        public void Dispose()
        {
            this.serviceProvider = null;
        }
    }
}
