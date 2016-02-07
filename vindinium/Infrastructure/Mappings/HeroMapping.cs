using AutoMapper;

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
                .ConstructUsing(o => new HeroNode(this.GetTileType(o), o.pos.y, o.pos.x)
                                         {
                                             Id = o.id,
                                             Elo = o.elo,
                                             Gold = o.gold,
                                             Life = o.life,
                                             Passable = false,
                                             Crashed = o.crashed,
                                             MineCount = o.mineCount
                                         })               
                .ForAllMembers(o => o.Ignore());


            CreateMap<Hero, VillianNode>()
                    .ConstructUsing(o => new VillianNode(this.GetTileType(o), o.pos.y, o.pos.x)
                    {
                        Id = o.id,
                        Elo = o.elo,
                        Gold = o.gold,
                        Life = o.life,
                        Passable = false,
                        Crashed = o.crashed,
                        MineCount = o.mineCount
                    })
                .ForAllMembers(o => o.Ignore());
        }

        private Tile GetTileType(Hero hero)
        {
            switch (hero.id)
            {
                case 1:
                    return Tile.HERO_1;
                case 2:
                    return Tile.HERO_2;
                case 3:
                    return Tile.HERO_3;
                case 4:
                    return Tile.HERO_4;
                default:
                    return Tile.FREE;
            }
        }
    }
}