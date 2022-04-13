using AutoMapper;
using BlogProject.Model.Entities.Concrete;
using BlogProject.Web.Areas.Member.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogProject.Web.Models.Mappers
{
    public class Mapping :Profile
    {
        public Mapping()
        {
            CreateMap<AppUser, CreateAppUserDTO>().ReverseMap();

            CreateMap<CreateCategoryDTO, Category>().ReverseMap();

            CreateMap<Category, UpdateCategoryDTO>().ReverseMap();


            CreateMap<CreateArticleDTO, Article>().ReverseMap();
            CreateMap<UpdateArticleDTO, Article>().ReverseMap();  // reversemap: iki yönlü çalışma sağlar.

            CreateMap<UpdateProfileDTO, AppUser>().ReverseMap();

            CreateMap<CreateCommentDTO, Comment>().ReverseMap();

            CreateMap<CreateLikeDTO, Like>().ReverseMap();
        }

    }
}
