using System;
using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Basic.Swagger;
using Microsoft.AspNetCore.Localization;
using System.IO;
using IGeekFan.AspNetCore.Knife4jUI;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace Basic
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
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "Test API V1",
                        Version = "v1",
                        Description = "A sample API for testing Swashbuckle",
                        TermsOfService = new Uri("http://tempuri.org/terms")
                    }
                );
                c.SwaggerDoc("gp", new OpenApiInfo { Title = "µÇÂ¼Ä£¿é", Version = "GP" });
                c.RequestBodyFilter<AssignRequestBodyVendorExtensions>();

                c.OperationFilter<AssignOperationVendorExtensions>();

                c.SchemaFilter<ExamplesSchemaFilter>();

                c.DescribeAllParametersInCamelCase();

                c.GeneratePolymorphicSchemas();

                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Basic.xml"),true);

                //c.EnableAnnotations();
                c.AddServer(new OpenApiServer()
                {
                    Url = "",
                    Description = "vvv"
                });
                c.CustomOperationIds(apiDesc =>
                {
                    var controllerAction = apiDesc.ActionDescriptor as ControllerActionDescriptor;
                    return controllerAction.ControllerName + "-" + controllerAction.ActionName;
                });

                c.OrderActionsBy((apiDesc) => $"{apiDesc.ActionDescriptor.RouteValues["controller"]}_{apiDesc.HttpMethod}");
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseRouting();


            var supportedCultures = new[]
            {
                new CultureInfo("en-US"),
                new CultureInfo("fr"),
                new CultureInfo("sv-SE"),
            };

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en-US"),
                // Formatting numbers, dates, etc.
                SupportedCultures = supportedCultures,
                // UI strings that we have localized.
                SupportedUICultures = supportedCultures
            });

            app.UseSwagger(c =>
            {
            });

            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = ""; // serve the UI at root
                c.SwaggerEndpoint("/v1/api-docs", "V1 Docs");
                c.SwaggerEndpoint("/gp/api-docs", "µÇÂ¼Ä£¿é");
            });
            app.UseKnife4UI(c =>
            {
                //c.RoutePrefix = ""; // serve the UI at root
                c.SwaggerEndpoint("/v1/api-docs", "V1 Docs");
                c.SwaggerEndpoint("/gp/api-docs", "µÇÂ¼Ä£¿é");
            });


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapSwagger("{documentName}/api-docs");

            });
        }
    }
}
