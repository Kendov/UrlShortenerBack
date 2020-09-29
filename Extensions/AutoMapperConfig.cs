using System;
using AutoMapper;
using urlShortener.Models;

namespace urlShortener.Extensions
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<UrlDataEntryModel, UrlData>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.CustomUrl))
            .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Url))
            .ForMember(dest => dest.ExpireAt, opt => opt.MapFrom(src => DateTime.Now.AddSeconds(src.ExpirationTime)));
            
        }
    }
}