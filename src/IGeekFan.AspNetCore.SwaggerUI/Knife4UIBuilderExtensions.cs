using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;


namespace IGeekFan.AspNetCore.Knife4jUI
{
    public static class Knife4UIBuilderExtensions
    {
        public static IApplicationBuilder UseKnife4UI(
             this IApplicationBuilder app,
             Action<Knife4UIOptions> setupAction = null)
        {
            var options = new Knife4UIOptions();
            if (setupAction != null)
            {
                setupAction(options);
            }
            else
            {
                options = app.ApplicationServices.GetRequiredService<IOptions<Knife4UIOptions>>().Value;
            }
            
            app.UseMiddleware<Knife4jUIMiddleware>(options);

            return app;
        }
    }
}
