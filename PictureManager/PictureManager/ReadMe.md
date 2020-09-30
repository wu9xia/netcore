客户端项目（Blazor项目）
1. 需要修改Program的基地址,其中的BaseAddress是接口的地址,可以改成从配置文件获取
builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri("https://localhost:44350/") });


Blazor入门笔记
https://www.cnblogs.com/zxyao/archive/2004/01/13/12671873.html 系列文章
