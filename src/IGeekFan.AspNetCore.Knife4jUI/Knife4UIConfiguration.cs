using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;

namespace IGeekFan.AspNetCore.Knife4jUI
{
    //热更新配置类
    internal static class Knife4UIConfiguration
    {
        /// <summary>
        /// 是否显示Swagger
        /// </summary>
        public static string Password
        {
            get; set;
        }

        public static bool Authentication { get; set; } = false;

        //动态类的扩展方法
        public static void AddDynamicConfig(IConfiguration configuration)
        {
            ChangeToken.OnChange(() => configuration?.GetReloadToken(), () =>
            {
                //热更新
                if (configuration != null)
                {
                    Init(configuration);
                }
            });
            Init(configuration);
        }

        private static void Init(IConfiguration configuration)
        {
            var knife4JAuth = configuration["Knife4jUIConfig:Knife4JAuth"];
            if (!string.IsNullOrWhiteSpace(knife4JAuth))
            {
                Authentication = Convert.ToBoolean(knife4JAuth);
                Password = configuration["Knife4jUIConfig:Knife4JPassword"];
            }

        }
    }
}