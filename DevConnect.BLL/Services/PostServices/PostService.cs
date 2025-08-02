using AutoMapper;
using DevConnect.BLL.DTOs.PostDTOs;
using DevConnect.Domain.Entities;
using DevConnect.Repositories.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevConnect.BLL.Services.PostServices
{
    public class PostService : IPostService
    {
        private readonly IGenericRepository<Post> Repository;
        private readonly IMapper Mapper;

        public PostService(IGenericRepository<Post> postRepository, IMapper mapper)
        {
            Repository = postRepository;
            Mapper = mapper;
        }
        public async Task<List<GetPostDto>> GetAllAsync() =>
        Mapper.Map<List<GetPostDto>>(await Repository.GetAllAsync());

        public async Task<GetPostDto> GetByIdAsync(Guid id) =>
            Mapper.Map<GetPostDto>(await Repository.GetByIdAsync(id));

        public async Task CreateAsync(CreatePostDto dto)
        {
            var post = Mapper.Map<Post>(dto);
            post.CreatedAt = DateTime.UtcNow;
            await Repository.CreateAsync(post);
        }

        public async Task UpdateAsync(Guid id, UpdatePostDto dto)
        {
            var post = await Repository.GetByIdAsync(id);
            if (post == null)
            {
                throw new Exception($"Post with ID {id} not found.");
            }
            Mapper.Map(dto, post);
            await Repository.UpdateAsync(post);
        }

        public async Task DeleteAsync(Guid id) =>
            await Repository.DeleteAsync(id);

    }
}
