using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Models;

namespace API.Helper
{
    public class AutomapperProfiler : Profile
    {
        public AutomapperProfiler()
        {
            CreateMap<Daily, DailyDto>().ReverseMap();
            //  .ForMember(src => src.DailyBoxes.Select(x => x.Forms.Sum(x => x.SumTax)), opt => opt.MapFrom(dest => dest.Total));
            CreateMap<DailyBoxes, DailyBoxDto>().ReverseMap();
            CreateMap<Form, FormDto>().ReverseMap();

            CreateMap<Box, BoxDto>().ReverseMap();
            CreateMap<Collage, CollageDto>().ReverseMap();

            // CreateMap<DailyDto, Daily>()
            //  .ForMember(src => src.DailyBoxes.Select(t => t.Forms.Sum(j => j.SumTax)), opt => opt.MapFrom(dest => dest.Total));
        }

    }
}