using Com.Moonlay.Service.Project.Lib;
using Com.Moonlay.Service.Project.Lib.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Moonlay.Service.Project.Test
{
    public class ServiceProviderFixture : IDisposable
    {
        public IServiceProvider ServiceProvider { get; private set; }
        public ServiceProviderFixture()
        {
            var connectionString = @"Server=tcp:127.0.0.1,1401;Database=com.moonlay.db.project;User=sa;password=Standar123.;MultipleActiveResultSets=true;Persist Security Info=True";
            this.ServiceProvider = new ServiceCollection()
                .AddDbContext<ProjectDbContext>(options => options.UseSqlServer(connectionString))
                .AddSingleton<ProjectService>()
                .AddSingleton<BacklogService>()
                .AddSingleton<HelperService>()
                .BuildServiceProvider();

            ProjectDbContext dbContext = ServiceProvider.GetService<ProjectDbContext>();
            dbContext.Database.Migrate();

            HelperService h = ServiceProvider.GetService<HelperService>();
        }

        public void Dispose()
        { 
        }
    }

    [CollectionDefinition("ServiceProviderFixture collection")]
    public class ServiceProviderFixtureCollection : ICollectionFixture<ServiceProviderFixture>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }
}
