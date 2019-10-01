using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using Lamar;
using System.Diagnostics;
using RiskFirst.Hateoas;
using HowToHATEOAS.Core.Domain.Model;
//using HowToHATEOAS.API.HAL;
using Newtonsoft.Json;
using AutoMapper;

namespace HowToHATEOAS.API
{
	public class Startup
	{
		/*
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Player API", Version = "v1" });
            });
        }
        */

		public void ConfigureContainer(ServiceRegistry services)
		{
			// Supports ASP.Net Core DI abstractions
			services.AddMvc(options =>
			{
				//options.OutputFormatters.Add(new HalOutputFormatter(new JsonSerializerSettings(), System.Buffers.ArrayPool<char>.Shared));
			}).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
			services.AddLogging();
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new Info { Title = "Player API", Version = "v1" });
			});

			// Also exposes Lamar specific registrations
			// and functionality
			services.Scan(s =>
			{
				s.TheCallingAssembly();
				s.AssembliesFromApplicationBaseDirectory(x => x.FullName.Contains("HowToHATEOAS"));
				s.LookForRegistries();
				s.WithDefaultConventions();
			});

			//services.AddAutoMapper(;

			// RiskFirst.Hateoas configuration
			services.AddLinks(config =>
			{
				config.AddPolicy<Player>(policy =>
				{
					policy
						  //.RequireSelfLink()
						  .RequireRoutedLink("all", "GetPlayers")
						  .RequireRoutedLink("status_active", "UpdatePlayerStatus", x => new { id = x.Id, status = "1" }, c => c.Assert(x => x.AvailableStatuses.Any(y => y.Id == 1)))
						  .RequireRoutedLink("status_injured", "UpdatePlayerStatus", x => new { id = x.Id, status = "2" }, c => c.Assert(x => x.AvailableStatuses.Any(y => y.Id == 2)))
						  .RequireRoutedLink("status_suspended", "UpdatePlayerStatus", x => new { id = x.Id, status = "3" }, c => c.Assert(x => x.AvailableStatuses.Any(y => y.Id == 3)))
						  .RequireRoutedLink("status_retired", "UpdatePlayerStatus", x => new { id = x.Id, status = "4" }, c => c.Assert(x => x.AvailableStatuses.Any(y => y.Id == 4)));
				});
			});
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
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseMvc();
			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "Player API V1");
			});
		}
	}
}
