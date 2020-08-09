# IGeekFan.AspNetCore.Knife4jUI
一个支持.NET Core3.0,.NET Standard2.0的swagger ui 库：**knife4j UI**。

## 相关类库
### [knife4j](https://gitee.com/xiaoym/knife4j)
- knife4j-vue-v3(不是vue3,而是swagger-ui-v3版本）
### [Swashbuckle.AspNetCore](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)

依赖项
- Swashbuckle.AspNetCore.Swagger
- Swashbuckle.AspNetCore.SwaggerGen
```
   <PackageReference Include="Microsoft.AspNetCore.Routing" Version="2.1.0" />
   <PackageReference Include="Microsoft.AspNetCore.StaticFiles" Version="2.1.0" />
   <PackageReference Include="Microsoft.Extensions.FileProviders.Embedded" Version="2.1.0" />
   <PackageReference Include="System.Text.Json" Version="4.6.0" />
```
## demo
-[https://github.com/luoyunchong/IGeekFan.AspNetCore.Knife4jUI/blob/master/test/Basic](https://github.com/luoyunchong/IGeekFan.AspNetCore.Knife4jUI/blob/master/test/Basic)

## 快速开始

### 安装包

```
Install-Package IGeekFan.AspNetCore.Knife4jUI
```
或
```
dotnet add package IGeekFan.AspNetCore.Knife4jUI
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
 app.UseSwagger(c =>
            {
              
            });

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
