using System.Collections.Generic;

namespace vindinium.common.Dtos
{
    public class Game
    {
        public Game()
        {
            Rounds = new List<Round>();
        }

        public string Id { get; set; }
        public List<Round> Rounds { get; set; }
    }
}