using System.Net;
using System.Text;
using Api.Interfaces;
using Api.Services;
using Api.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();

builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

//ngrok
if(builder.Environment.IsDevelopment())
    builder.Services.AddHostedService<TunnelService>();

//utils
builder.Services.AddScoped<IJwtHandler, JwtHandler>();

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

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseCors(_ => _.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

//swagger
app.UseSwagger();
app.UseSwaggerUI(_ => {

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
