var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();


builder.Services.AddControllers();


var app = builder.Build();

app.UseCors(_ => _.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());



app.MapControllers();
app.Run();
