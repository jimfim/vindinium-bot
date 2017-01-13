using AutoMapper;
using vindinium.common.Dtos;
using vindinium.common.Entities;

namespace vindinium.common.Mappings
{
    public class EntityMapper : Profile
    {
        public EntityMapper()
        {
            CreateMap<BoardDTO, Board>()
                .ForMember(x => x.Size, y => y.MapFrom(n => n.Size))
                .ForMember(x => x.Tiles, y => y.MapFrom(n => n.Tiles));

            CreateMap<HeroDTO, Hero>()
                .ForMember(x => x.Crashed, y => y.MapFrom(n => n.Crashed))
                .ForMember(x => x.Elo, y => y.MapFrom(n => n.Elo))
                .ForMember(x => x.Gold, y => y.MapFrom(n => n.Gold))
                .ForMember(x => x.MineCount, y => y.MapFrom(n => n.MineCount))
                .ForMember(x => x.Pos, y => y.MapFrom(n => n.PosDto))
                .ForMember(x => x.SpawnPos, y => y.MapFrom(n => n.SpawnPosDto))
                .ForMember(x => x.LastDir, y => y.MapFrom(n => n.LastDir))
                .ForMember(x => x.Name, y => y.MapFrom(n => n.Name))
                .ForMember(x => x.UserId, y => y.MapFrom(n => n.UserId));

            CreateMap<RoundDTO, Round>()
                .ForMember(x => x.Board, y => y.MapFrom(n => n.BoardDto))
                .ForMember(x => x.Finished, y => y.MapFrom(n => n.Finished))
                .ForMember(x => x.MaxTurns, y => y.MapFrom(n => n.MaxTurns))
                .ForMember(x => x.Turn, y => y.MapFrom(n => n.Turn))
                .ForMember(x => x.Reference, y => y.MapFrom(n => n.Id))
                //.ForMember(x => x.Heroes, y => y.MapFrom(n => n.Heroes))
                .ForMember(x => x.Id, y => y.Ignore());

            CreateMap<PosDTO, Pos>()
                .ForMember(x => x.X, y => y.MapFrom(n => n.X))
                .ForMember(x => x.Y, y => y.MapFrom(n => n.Y));

            CreateMap<SpawnPosDTO, Pos>()
                .ForMember(x => x.X, y => y.MapFrom(n => n.X))
                .ForMember(x => x.Y, y => y.MapFrom(n => n.Y));
        }
        
    }
}
