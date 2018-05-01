using AutoMapper;
using CleanArchitecture.Core.DTO;
using CleanArchitecture.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.ClientWeb
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ArticleDTO, ArticleEntity>().ReverseMap();
            CreateMap<ArticleCategoryDTO, ArticleCategoryEntity>().ReverseMap();
        }
    }
}
