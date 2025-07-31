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

// 2. Connection string va DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// 3. AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// 4. Repository'larni DI ga qo‘shish
builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<ISkillRepository, SkillRepository>();
// kerak bo‘lsa yana qo‘shiladi: ICommentRepository va hokazo

// 5. Servicelarni DI ga qo‘shish
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<ISkillService, SkillService>();
// kerak bo‘lsa yana qo‘shiladi: ICommentService va hokazo

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
