using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EatHealthyApi.Data;
using EatHealthyApi.Data.RefrentialData;
using EatHealthyApi.Data.Users;
using EatHealthyApi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;

namespace EatHealthyApi
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
            services.AddDbContext<EhDBContext>(opt => 
            opt.UseSqlServer(Configuration.GetConnectionString("EatHealthyConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>(
                options =>
                {                        
                    options.User.AllowedUserNameCharacters = string.Empty;
                }).AddEntityFrameworkStores<EhDBContext>();

            //services.AddAuthentication().

            services.AddControllers().AddNewtonsoftJson(s => {
            s.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });
            services.AddScoped<IEHUserRepo, SqlEHUserRepo>();
            services.AddScoped<IDislikeDataRepo, DislikeDataRepo>();
            services.AddScoped<IFoodPreferenceDataRepo, FoodPreferenceDataRepo>();
            services.AddScoped<IGoalsDataRepo, GoalsDataRepo>();
            services.AddScoped<IActivityTypeDataRepo, ActivityTypeDataRepo>();
            services.AddScoped<IVarietyTypeDataRepo, VarietyTypeDataRepo>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Eat Healthy API");
            });
            
            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
