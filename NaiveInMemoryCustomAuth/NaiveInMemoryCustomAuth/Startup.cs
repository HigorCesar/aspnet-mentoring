using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NaiveInMemoryCustomAuth.Infrastructure;

namespace NaiveInMemoryCustomAuth
{
    public class Startup
    {
        private readonly Authenticator authenticator;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            authenticator = new Authenticator();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<Authenticator>(authenticator);
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            NaiveAuthMiddleware authMiddleware = new NaiveAuthMiddleware(authenticator);
            app.Use(authMiddleware.Authenticate);
            app.UseMvc();
        }
    }
}
