using DevConnect.BLL;
using DevConnect.BLL.DTOs.UserDTOs;
using DevConnect.BLL.Services;
using DevConnect.BLL.Services.CommentServices;
using DevConnect.BLL.Services.FollowServices;
using DevConnect.BLL.Services.LikeServices;
using DevConnect.BLL.Services.MessageServices;
using DevConnect.BLL.Services.NotificationServices;
using DevConnect.BLL.Services.PostServices;
using DevConnect.BLL.Services.ProjectServices;
using DevConnect.BLL.Services.SkillServices;
using DevConnect.BLL.Services.UserServices;
using DevConnect.BLL.Validators.CommentValidator;
using DevConnect.BLL.Validators.FollowValidator;
using DevConnect.BLL.Validators.LikeValidator;
using DevConnect.BLL.Validators.MessageValidator;
using DevConnect.BLL.Validators.NotificationValidator;
using DevConnect.BLL.Validators.PostValidator;
using DevConnect.BLL.Validators.ProjectValidator;
using DevConnect.BLL.Validators.SkillValidator;
using DevConnect.BLL.Validators.UserValidator;
using DevConnect.Repositories.Repositories;
using DevConnect.Repositories.Repositories.COMMENT;
using DevConnect.Repositories.Repositories.FOLLOW;
using DevConnect.Repositories.Repositories.MESSAGE;
using DevConnect.Repositories.Repositories.NOTIFICATION;
using DevConnect.Repositories.Repositories.POST;
using DevConnect.Repositories.Repositories.PROJECT;
using DevConnect.Repositories.Repositories.SKILL;
using DevConnect.Repositories.Repositories.USER;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace DevConnect.Configurations
{
    public static class DependicyInjectionConfiguration
    {
        public static void ConfigureDI(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ICommentRepository, CommentRepository>();
            builder.Services.AddScoped<IFollowRepository, FollowRepository>();
            builder.Services.AddScoped<IMessageRepository, MessageRepository>();
            builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
            builder.Services.AddScoped<IPostRepository, PostRepository>();
            builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
            builder.Services.AddScoped<ISkillRepository, SkillRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();


            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<ISkillService, SkillService>();
            builder.Services.AddScoped<IProjectService, ProjectService>();
            builder.Services.AddScoped<IPostService, PostService>();
            builder.Services.AddScoped<INotificationService, NotificationService>();
            builder.Services.AddScoped<IMessageService, MessageService>();
            builder.Services.AddScoped<ILikeService, LikeService>();
            builder.Services.AddScoped<IFollowService, FollowService>();
            builder.Services.AddScoped<ICommentService, CommentService>();


            builder.Services.AddValidatorsFromAssemblyContaining<CreateUserDtoValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<UpdateUserDtoValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<CreateSkillDtoValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<UpdateSkillDtoValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<CreateProjectDtoValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<UpdateProjectDtoValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<CreatePostDtoValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<UpdatePostDtoValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<CreateNotificationDtoValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<CreateMessageDtoValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<CreateLikeDtoValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<CreateFollowDtoValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<CreateCommentDtoValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<UpdateCommentDtoValidator>();


        }
    }
}
 