using System.Collections.Generic;

namespace vindinium.common.Entities
{
    public class Game
    {
        public virtual int Id { get; protected set; }

        public virtual Board Board { get; protected set; }

        public virtual bool Finished { get; protected set; }

        public virtual List<Hero> Heroes { get; protected set; }

        public virtual string Reference { get; protected set; }

        public virtual int MaxTurns { get; protected set; }

        public virtual int Turn { get; protected set; }

        public Game()
        {
            Heroes = new List<Hero>();
        }
    }
}