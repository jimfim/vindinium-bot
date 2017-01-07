using System.Collections.Generic;

namespace vindinium.common.Dtos
{
    public class GameDTO
    {
        public GameDTO()
        {
            Rounds = new List<RoundDTO>();
        }

        public string Id { get; set; }
        public List<RoundDTO> Rounds { get; set; }
    }
}