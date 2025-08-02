using AutoMapper;
using DevConnect.BLL.DTOs.CommentDTOs;
using DevConnect.BLL.DTOs.FollowDTOs;
using DevConnect.BLL.DTOs.LikeDTOs;
using DevConnect.BLL.DTOs.MessageDTOs;
using DevConnect.BLL.DTOs.NotificationDTOs;
using DevConnect.BLL.DTOs.PostDTOs;
using DevConnect.BLL.DTOs.ProjectDTOs;
using DevConnect.BLL.DTOs.SkillDTOs;
using DevConnect.BLL.DTOs.UserDTOs;
using DevConnect.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevConnect.BLL
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, GetUserDto>();
            CreateMap<CreateUserDto, User>();
            CreateMap<UpdateUserDto, User>();


            CreateMap<Skill, GetSkillDto>();
            CreateMap<CreateSkillDto, Skill>();
            CreateMap<UpdateSkillDto, Skill>();


            CreateMap<Project, GetProjectDto>();
            CreateMap<CreateProjectDto, Project>();
            CreateMap<UpdateProjectDto, Project>();


            CreateMap<Post, GetPostDto>()
                .ForMember(dest => dest.AuthorUserName, opt => opt.MapFrom(src => src.User.Username));
            CreateMap<CreatePostDto, Post>();
            CreateMap<UpdatePostDto, Post>();


            CreateMap<Comment, GetCommentDto>()
                .ForMember(dest => dest.AuthorUserName, opt => opt.MapFrom(src => src.Author.Username));
            CreateMap<CreateCommentDto, Comment>();
            CreateMap<UpdateCommentDto, Comment>();


            CreateMap<Like, GetLikeDto>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User.Username));
            CreateMap<CreateLikeDto, Like>();


            CreateMap<Follow, GetFollowDto>();
            CreateMap<CreateFollowDto, Follow>();


            CreateMap<Message, GetMessageDto>()
                .ForMember(dest => dest.SenderUserName, opt => opt.MapFrom(src => src.Sender.Username))
                .ForMember(dest => dest.ReceiverUserName, opt => opt.MapFrom(src => src.Receiver.Username));
            CreateMap<CreateMessageDto, Message>();


            CreateMap<Notification, GetNotificationDto>();
            CreateMap<CreateNotificationDto, Notification>();

        }
    }
}
