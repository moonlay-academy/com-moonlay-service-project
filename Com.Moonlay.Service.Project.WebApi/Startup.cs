using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Com.Moonlay.Service.Project.Lib;
using Com.Moonlay.Service.Project.Lib.Services;
using IdentityServer4.AccessTokenValidation;

namespace Com.Moonlay.Service.Project.WebApi
{
    public class Startup
    { 
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            var connectionString = Configuration.GetConnectionString("DefaultConnection") ?? Configuration["DefaultConnection"];
            services
                .AddDbContext<ProjectDbContext>(options => options.UseSqlServer(connectionString))
                .AddSingleton<ProjectService>();

            string authority = Configuration.GetValue<string>("Authority");
            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = authority;
                    options.RequireHttpsMetadata = false; // only for development
                    options.ApiName = "com.moonlay.service.project";
                });

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var forwardOptions = new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto,
                RequireHeaderSymmetry = false
            };

            forwardOptions.KnownNetworks.Clear();
            forwardOptions.KnownProxies.Clear();

            // ref: https://github.com/aspnet/Docs/issues/2384
            app.UseForwardedHeaders(forwardOptions);


            //string authority = Configuration.GetValue<string>("Authority");
            //app.UseIdentityServerAuthentication(new IdentityServerAuthenticationOptions
            //{
            //    Authority = authority,
            //    RequireHttpsMetadata = false,
            //    ApiName = "com.moonlay.service.project",
            //    AllowedScopes = { "service.project.read", "service.project.write" }
            //});
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
