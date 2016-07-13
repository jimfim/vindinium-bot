using System.Collections.Generic;

namespace vindinium.common.Dtos
{
    public class Round
    {
        public string Id { get; set; }
        public int Turn { get; set; }
        public int MaxTurns { get; set; }
        public List<Hero> Heroes { get; set; }
        public Board Board { get; set; }
        public bool Finished { get; set; }
    }
}