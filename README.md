# IGeekFan.AspNetCore.Knife4jUI
一个支持.NET Core3.0,.NET Standard2.0的swagger ui 库：**knife4j UI**。

## 相关依赖项
### [knife4j](https://gitee.com/xiaoym/knife4j)
- knife4j-vue-v3(不是vue3,而是swagger-ui-v3版本）
### [Swashbuckle.AspNetCore](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)
- Swashbuckle.AspNetCore.Swagger
- Swashbuckle.AspNetCore.SwaggerGen

## Demo
- [Basic](https://github.com/luoyunchong/IGeekFan.AspNetCore.Knife4jUI/blob/master/test/Basic)
- [Knife4jUIDemo](https://github.com/luoyunchong/IGeekFan.AspNetCore.Knife4jUI/blob/master/test/Knife4jUIDemo)

## 快速开始

### 安装包

1.Install the standard Nuget package into your ASP.NET Core application.

```
Package Manager : Install-Package IGeekFan.AspNetCore.Knife4jUI
CLI : dotnet add package IGeekFan.AspNetCore.Knife4jUI
```

2.In the ConfigureServices method of Startup.cs, register the Swagger generator, defining one or more Swagger documents.

```
using System.Reflection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using IGeekFan.AspNetCore.Knife4jUI;
```
### ConfigureServices

CustomOperationIds
AddServer,必须的。
```
   services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1",new OpenApiInfo{Title = "API V1",Version = "v1"});
        c.AddServer(new OpenApiServer()
        {
            Url = "",
            Description = "vvv"
        });
        c.CustomOperationIds(apiDesc =>
        {
            return apiDesc.TryGetMethodInfo(out MethodInfo methodInfo) ? methodInfo.Name : null;
        });
    });
```

### Configure

```
app.UseSwagger();

app.UseKnife4UI(c =>
{
    c.RoutePrefix = ""; // serve the UI at root
    c.SwaggerEndpoint("/v1/api-docs", "V1 Docs");
});

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapSwagger("{documentName}/api-docs");
});
```


### 效果图
运行项目，打开 https://localhost:5001/index.html#/home

![https://pic.downk.cc/item/5f2fa77b14195aa594ccbedc.jpg](https://pic.downk.cc/item/5f2fa77b14195aa594ccbedc.jpg)


### 更多配置请参考

- [https://github.com/domaindrivendev/Swashbuckle.AspNetCore](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)


### 更多项目

- [https://api.igeekfan.cn/swagger/index.html](https://api.igeekfan.cn/swagger/index.html)