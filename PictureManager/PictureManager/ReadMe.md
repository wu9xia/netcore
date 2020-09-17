客户端项目（Blazor项目）
1. 需要修改Program的基地址,其中的BaseAddress是接口的地址,可以改成从配置文件获取
builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri("https://localhost:44350/") });
