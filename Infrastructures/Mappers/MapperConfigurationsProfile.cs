using Application.Commons;
using Application.ViewModels.AccountDTOs;
using AutoMapper;
using Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructures.Mappers
{
    public class MapperConfigurationsProfile : Profile
    {
        public MapperConfigurationsProfile()
        {
            // Authentication
            CreateMap<User, AccountDTO>().ReverseMap();
            CreateMap<User, AuthenAccountDTO>().ReverseMap();
            CreateMap<User, RegisterAccountDTO>().ReverseMap();
            //Pagination
            CreateMap(typeof(Pagination<>), typeof(Pagination<>));
        }
    }
}
