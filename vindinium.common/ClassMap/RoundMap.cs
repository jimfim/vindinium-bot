using FluentNHibernate.Mapping;
using vindinium.common.Entities;

namespace vindinium.common.ClassMap
{
    public class RoundMap : ClassMap<Round>
    {
        public RoundMap()
        {
            Id(x => x.Id);
            //HasMany(x => x.Heroes);
            References(x => x.Board).Cascade.SaveUpdate();
            Map(x => x.Finished);
            Map(x => x.MaxTurns);
            Map(x => x.Turn);
            Map(x => x.Reference);
        }
    }
}
