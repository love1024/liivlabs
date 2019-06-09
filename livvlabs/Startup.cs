using liivlabs_infrastructure.EntityFramework;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using liivlabs_shared.Interfaces.Repository.Account;
using liivlabs_shared.Interfaces.Services.Account;
using liivlabs_infrastructure.Repositories.Account;
using liivlabs_core.Services.Account;
using AutoMapper;
using liivlabs_core.Mapper.Account;
using System;

namespace livvlabs
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

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddCors();

            // Auto Mapper Configurations, Adding all mapping profiles
            // by scanning one and getting its assembly, which is liivlab-core
            // Auto mapper will scan all and will get classes which inherit profile
            var mappingConfig = new MapperConfiguration(mc => {
                mc.AddMaps(typeof(UserProfile));
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            //Register Repositories
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IAccountService, AccountService>();


            //Sql server setup
            string connection = this.Configuration["ConnectionString"];
            services.AddDbContext<DatabaseContext>
                (options => options.UseSqlServer(connection));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseCors();
            app.UseMvc();
        }
    }
}
