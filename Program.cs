using DevConnect.BLL;
using DevConnect.BLL.Services.PostServices;
using DevConnect.BLL.Services.ProjectServices;
using DevConnect.BLL.Services.SkillServices;
using DevConnect.BLL.Services.UserServices;
using DevConnect.DataAccess;
using DevConnect.Repositories.Repositories.POST;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// 1. Serilog konfiguratsiyasi (ixtiyoriy)
builder.Host.UseSerilog((context, config) =>
{
    config.WriteTo.Console()
          .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day);
});


// 6. Controller va Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 7. Swagger va Exception handling
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 8. Middlewarelar
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
