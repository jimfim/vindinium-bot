using FluentNHibernate.Mapping;
using vindinium.common.Entities;

namespace vindinium.common.ClassMap
{
    public class HeroMap : ClassMap<Hero>
    {
        public HeroMap()
        {
            Id(x => x.Id);
            Map(x => x.Crashed);
            Map(x => x.Elo);
            Map(x => x.Gold);
            Map(x => x.LastDir);
            Map(x => x.MineCount);
            References(x => x.Pos).Cascade.SaveUpdate();
            References(x => x.SpawnPos).Cascade.SaveUpdate();
            Map(x => x.UserId);
            Map(x => x.Name);
        }
    }
}
