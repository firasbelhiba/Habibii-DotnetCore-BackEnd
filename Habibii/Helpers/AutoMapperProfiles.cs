using AutoMapper;
using Habibii.Dtos;
using Habibii.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Habibii.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserForListDto>()
                .ForMember(destination => destination.PhotoUrl, option =>
               {
                   option.MapFrom(source => source.Photos.FirstOrDefault(p => p.IsMain).Url);
               })
               .ForMember(destination => destination.Age, option =>
               {
                   option.ResolveUsing(d => d.DateOfBirth.CalculateAge());
               });
            CreateMap<User, UserForDetailedDto>()
                .ForMember(destination => destination.PhotoUrl, option =>
                {
                    option.MapFrom(source => source.Photos.FirstOrDefault(p => p.IsMain).Url);
                })
                .ForMember(destination => destination.Age, option =>
               {
                   option.ResolveUsing(d => d.DateOfBirth.CalculateAge());
               });
            CreateMap<Photo, PhotosForDetailedDto>();
            CreateMap<UserForUpdateDto, User>();
            CreateMap<PhotoForCreationDto, Photo>();
            CreateMap<Photo, PhotoForReturnDto>();

        }
    }
}
