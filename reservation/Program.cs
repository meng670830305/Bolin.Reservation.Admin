using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Model.Other;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SqlSugar;
using System.Text;
using webapi.Config;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    // 设置标题和版本
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Bolin.Admin.API", Version = "v1" });

    //添加安全定义
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "请输入token,格式为 Bearer xxxxxxxx（注意中间必须有空格）",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    //添加安全要求
    options.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme{
                            Reference =new OpenApiReference{
                                Type = ReferenceType.SecurityScheme,
                                Id ="Bearer"
                            }
                        },Array.Empty<string>()
                    }
     });
    //设置对象类型参数默认值
    options.SchemaFilter<DefaultValueSchemaFilter>();
});


//替换内置IOC
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(container =>
{
    #region 通过模块化的方法注册接口层和实现层
    container.RegisterModule(new AutofacModuleRegister());
    #endregion

    container.Register<ISqlSugarClient>(context =>
    {
        SqlSugarClient db = new SqlSugarClient(new ConnectionConfig()
        {
            DbType = DbType.SqlServer,
            IsAutoCloseConnection = true,
            ConnectionString = builder.Configuration.GetConnectionString("conn")
        });
        return db;
    });
});
//AutoMapper映射
builder.Services.AddAutoMapper(typeof(AutoMapperConfigs));
builder.Services.Configure<JWTTokenOptions>(builder.Configuration.GetSection("JWTTokenOptions"));
#region jwt校验 
{
    //第二步，增加鉴权逻辑
    JWTTokenOptions tokenOptions = new JWTTokenOptions();
    builder.Configuration.Bind("JWTTokenOptions", tokenOptions);
    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)//Scheme
     .AddJwtBearer(options =>  //这里是配置的鉴权的逻辑
     {
         options.TokenValidationParameters = new TokenValidationParameters
         {
             //JWT有一些默认的属性，就是给鉴权时就可以筛选了
             ValidateIssuer = true,//是否验证Issuer
             ValidateAudience = true,//是否验证Audience
             ValidateLifetime = false,//是否验证失效时间
             ValidateIssuerSigningKey = true,//是否验证SecurityKey
             ValidAudience = tokenOptions.Audience,//
             ClockSkew = TimeSpan.FromDays(1),//设置token过期后多久失效，默认过期后300秒内仍有效
             ValidIssuer = tokenOptions.Issuer,//Issuer，这两项和前面签发jwt的设置一致
             IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.SecurityKey))//拿到SecurityKey 
         };
     });

    builder.Services.AddAuthorization();
}
#endregion

// 设置JSON返回的日期格式
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    // 忽略循环引用
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    // 统一设置API的日期格式
    options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
    // 设置JSON返回格式同model一致（默认JSON里的首字母为小写，这里改为同后端Mode一致）
    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


////开启文件服务
//app.UseStaticFiles(new StaticFileOptions
//{
//    FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath, "wwwroot")),
//    RequestPath = "/static"
//});

app.UseHttpsRedirection();

//app.UseRouting();
//app.UseCors(x => x
//    .AllowAnyOrigin()
//    .AllowAnyMethod()
//    .AllowAnyHeader());

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();