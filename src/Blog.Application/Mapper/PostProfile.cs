using AutoMapper;
using Blog.Application.DTOs.PostDTOs;
using Blog.Domain.Entities;

namespace Blog.Application.Mapper
{
    public class PostProfile : Profile
    {
        public PostProfile()
        {
            CreateMap<PostDTO, Post>();
            CreateMap<Post, PostResponse>();
        }
    }
}
