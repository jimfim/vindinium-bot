using FluentNHibernate.Mapping;
using vindinium.common.Entities;

namespace vindinium.common.ClassMap
{
    public class RoundMap : ClassMap<Round>
    {
        public RoundMap()
        {
            Id(x => x.Id);
            Map(x => x.Heroes);
            Map(x => x.Board);
            Map(x => x.Finished);
            Map(x => x.MaxTurns);
            Map(x => x.Turn);
            Map(x => x.Reference);
        }
    }
}
