using AutoMapper;
using CleanArchitecture.Core.Data.DTO;
using CleanArchitecture.Core.Data.Entity;

namespace CleanArchitecture.Core.Mapping
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
