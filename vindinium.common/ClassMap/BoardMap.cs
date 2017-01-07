using FluentNHibernate.Mapping;
using vindinium.common.Entities;

namespace vindinium.common.ClassMap
{
    public class BoardMap : ClassMap<Board>
    {
        public BoardMap()
        {
            Id(x => x.Id);
            Map(x => x.Size);
            Map(x => x.Tiles);
        }
    }
}
