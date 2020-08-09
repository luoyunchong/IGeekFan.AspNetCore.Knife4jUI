# IGeekFan.AspNetCore.Knife4jUI
add knife4jui libs

### [knife4j](https://gitee.com/xiaoym/knife4j)

### [Swashbuckle.AspNetCore](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)

依赖项
- Swashbuckle.AspNetCore.Swagger
- Swashbuckle.AspNetCore.SwaggerGen

### []

## 安装包

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
