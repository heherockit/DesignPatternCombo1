using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using DesignPatternCombo1.Infrastructure.CommandQuery;
using DesignPatternCombo1.Infrastructure.Decorators;
using DesignPatternCombo1.Infrastructure.Mediator;
using DesignPatternCombo1.Web.Decorators;
using DesignPatternCombo1.Web.Features.Values;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SimpleInjector;
using SimpleInjector.Integration.AspNetCore.Mvc;
using SimpleInjector.Lifestyles;

namespace DesignPatternCombo1.Web
{
    public class Startup
    {
        private Container container = new Container(); 

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            IntegrateSimpleInjector(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            InitializeContainer(app);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }

        #region private

        private void IntegrateSimpleInjector(IServiceCollection services)
        {
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddSingleton<IControllerActivator>(
                new SimpleInjectorControllerActivator(container));
            services.AddSingleton<IViewComponentActivator>(
                new SimpleInjectorViewComponentActivator(container));

            services.EnableSimpleInjectorCrossWiring(container);
            services.UseSimpleInjectorAspNetRequestScoping(container);
        }

        private void InitializeContainer(IApplicationBuilder app)
        {
            // Add application presentation components:
            container.RegisterMvcControllers(app);
            container.RegisterMvcViewComponents(app);

            container.RegisterSingleton<IMediator, Mediator>();

            var assemblies = Assembly
                .GetEntryAssembly()
                .GetReferencedAssemblies()
                .Where(item => item.FullName.StartsWith("DesignPatternCombo1"))
                .Select(Assembly.Load)
                .Append(Assembly.GetEntryAssembly());

            container.Register(typeof(IQueryHandler<,>), assemblies, Lifestyle.Scoped);
            container.Register(typeof(ICommandHandler<>), assemblies, Lifestyle.Scoped);

            container.RegisterDecorator(typeof(IQueryHandler<,>), typeof(LogDecorator<,>));
            container.RegisterDecorator(typeof(IQueryHandler<,>), typeof(AppendToKeywordDecorator));
            
            // Allow Simple Injector to resolve services from ASP.NET Core.
            container.AutoCrossWireAspNetComponents(app);
        }

        #endregion
    }
}
