using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Knife4jUIDemo
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
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API V1", Version = "v1" });
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
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseStaticFiles();

            app.UseAuthorization();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = ""; // serve the UI at root
                c.SwaggerEndpoint("/v1/api-docs", "V1 Docs");//这个配置无效。
                c.IndexStream = () => new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")).GetFileInfo("index.html").CreateReadStream();
            });


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapSwagger("{documentName}/api-docs");
                endpoints.MapGet("/v3/api-docs/swagger-config", async (httpContext) =>
                {

                    JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions();
                    _jsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                    _jsonSerializerOptions.IgnoreNullValues = true;
                    _jsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase, false));

                    SwaggerUIOptions _options = new SwaggerUIOptions()
                    {
                        ConfigObject = new ConfigObject()
                        {
                            Urls = new List<UrlDescriptor>
                            {
                                new UrlDescriptor()
                                {
                                    Url="/v1/api-docs",
                                    Name="V1 Docs"
                                }
                            }
                        }
                    };

                    await httpContext.Response.WriteAsync(JsonSerializer.Serialize(_options.ConfigObject, _jsonSerializerOptions));
                });
            });
        }
    }
}
