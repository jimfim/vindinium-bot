using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            Map(x => x.Pos);
            Map(x => x.SpawnPos);
            Map(x => x.UserId);
            Map(x => x.Name);
        }
    }
}
