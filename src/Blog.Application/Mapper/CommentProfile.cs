using AutoMapper;
using Blog.Application.DTOs.CommentDTOs;
using Blog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Mapper
{
    public class CommentProfile : Profile
    {
        public CommentProfile()
        {
            CreateMap<CommentDTO, Comment>();
            CreateMap<Comment, CommentResponse>();  
        }
    }
}
