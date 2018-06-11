using AutoMapper;
using JT.Keep.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace JT.Keep.API
{
    public class AutoMapperConfig
    {
        public static void Initialize()
        {
            Mapper.Initialize((config) =>
            {
                config.CreateMap<DTO.Card, Card>().ReverseMap();
                config.CreateMap<DTO.ToDo, ToDo>().ReverseMap();
                config.CreateMap<DTO.Cooperator, Cooperator>().ReverseMap();
            });
        }
    }
}
