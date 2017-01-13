using System.Collections.Generic;

namespace vindinium.common.Entities
{
    public class Round
    {
        public virtual int Id { get; protected set; }

        public virtual Board Board { get; set; }

        public virtual bool Finished { get; set; }

        public virtual IList<Hero> Heroes { get; set; }

        public virtual string Reference { get; set; }

        public virtual int MaxTurns { get; set; }

        public virtual int Turn { get; set; }
    }
}