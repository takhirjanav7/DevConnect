using DevConnect.BLL;
using DevConnect.BLL.Services.PostServices;
using DevConnect.BLL.Services.ProjectServices;
using DevConnect.BLL.Services.SkillServices;
using DevConnect.BLL.Services.UserServices;
using DevConnect.Configurations;
using DevConnect.DataAccess;
using DevConnect.Repositories.Repositories.POST;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;


public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        LoggingConfiguration.ConfigureSerilog(builder);
        builder.ConfigureDatabase();
        builder.ConfigureDI();
       

        builder.Services.AddJwtAuthentication(builder.Configuration);


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
        app.UseAuthentication();
        app.UseAuthorization();
        app.ConfigureEndpoints();


        app.Run();

    }
}