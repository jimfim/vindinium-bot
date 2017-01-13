using FluentNHibernate.Mapping;
using vindinium.common.Entities;

namespace vindinium.common.ClassMap
{
    public class PosMap : ClassMap<Pos>
    {
        public PosMap()
        {
            Id(x => x.Id);
            Map(x => x.X);
            Map(x => x.Y);
        }
    }
}
