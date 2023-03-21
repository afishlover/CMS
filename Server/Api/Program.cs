using System.Net;
using System.Text;
using Api.Interfaces;
using Api.Interfaces.IServices;
using Api.Models;
using Api.Services;
using Api.Utils;
using InfrastructureLayer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();
builder.Services.AddSwaggerGen(conf =>
{
    conf.SwaggerDoc("v1", new OpenApiInfo
    {
        //Version = "v1",
        //Title = "Test API",
        //Description = "A simple example for swagger api information",
        //TermsOfService = new Uri("https://example.com/terms"),
        //Contact = new OpenApiContact
        //{
        //    Name = "Your Name XYZ",
        //    Email = "xyz@gmail.com",
        //    Url = new Uri("https://example.com"),
        //},
        //License = new OpenApiLicense
        //{
        //    Name = "Use under OpenApiLicense",
        //    Url = new Uri("https://example.com/license"),
        //}
    });
    conf.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });

    conf.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,

            //The name of the previously defined security scheme.
                    Id = "Bearer"
                }

            },
            new List<string>()
        }
    });

});
builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddControllers();

//db
builder.Services.AddOnion(builder.Configuration["ConnectionStrings:CMS"]);

//ngrok
// if(builder.Environment.IsDevelopment())
//     builder.Services.AddHostedService<TunnelService>();

//utils
builder.Services.AddOptions (); 
builder.Services.Configure<MailSetting>(builder.Configuration.GetSection("MailSetting"));  
builder.Services.AddScoped<IJwtHandler, JwtHandler>();
builder.Services.AddTransient<ISendMailService, SendMailService>();

//authentication scheme
builder.Services
    .AddAuthentication(auth =>
    {
        auth.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        auth.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
        auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JwtSetting:ValidIssuer"],
            ValidAudience = builder.Configuration["JwtSetting:ValidAudience"],
            IssuerSigningKey = new
                SymmetricSecurityKey
                (Encoding.UTF8.GetBytes
                    (builder.Configuration["JwtSetting:SecretKey"])),
            LifetimeValidator = (_, expires, _, _) =>
            {
                if (expires != null)
                {
                    return expires > DateTime.UtcNow;
                }

                return false;
            }
        };
    });

builder.Services.AddAuthorization(options =>
{
});

var app = builder.Build();

app.UseCors(_ => _.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

//swagger
app.UseSwagger();
app.UseSwaggerUI(conf =>
{
    conf.SwaggerEndpoint("/swagger/v1/swagger.json", "SwaggerDemoApplication V1");
    //conf.RoutePrefix = "";
});

//auth
app.UseAuthentication();
app.UseAuthorization();

//static files
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(System.IO.Path.Combine(builder.Environment.ContentRootPath, "Resources")),
    RequestPath = "/Resources",
    OnPrepareResponse = ctx =>
    {
        if (!ctx.Context.Request.Path.StartsWithSegments("/Resources")) return;
        ctx.Context.Response.Headers.Add("Cache-Control", "no-store");

        if (ctx.Context.User.Identity.IsAuthenticated) return;
        ctx.Context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
        ctx.Context.Response.ContentLength = 0;
        ctx.Context.Response.Body = Stream.Null;
    }
});

app.MapControllers();
app.Run();
