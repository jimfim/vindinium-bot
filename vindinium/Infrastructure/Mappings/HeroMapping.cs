﻿using AutoMapper;

using vindinium.Infrastructure.Behaviors.Map;
using vindinium.Infrastructure.Behaviors.Models;
using vindinium.Infrastructure.DTOs;

namespace vindinium.Infrastructure.Mappings
{
    internal class HeroMapping : Profile
    {
        /// <summary>
        ///     Override this method in a derived class and call the CreateMap method to associate that map with this profile.
        ///     Avoid calling the <see cref="T:AutoMapper.Mapper" /> class from this method.
        /// </summary>
        protected override void Configure()
        {
            CreateMap<Hero, HeroNode>()
                .ForMember(o => o.Life, m => m.MapFrom(s => s.life))
                .ForMember(o => o.Gold, m => m.MapFrom(s => s.gold))
                .ForMember(o => o.Crashed, m => m.MapFrom(s => s.crashed))
                .ForMember(o => o.Location, m => m.MapFrom(s => new CoOrdinates(s.pos.x, s.pos.y)))
                .ForMember(o => o.Elo, m => m.MapFrom(s => s.elo))
                .ForMember(o => o.MineCount, m => m.MapFrom(s => s.mineCount))
                .ForMember(o => o.Name, m => m.MapFrom(s => s.name))
                ;


        }
    }
}