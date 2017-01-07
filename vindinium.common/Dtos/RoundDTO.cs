using System.Collections.Generic;
using Newtonsoft.Json;

namespace vindinium.common.Dtos
{
    public class RoundDTO
    {
        public string Id { get; set; }
        public int Turn { get; set; }
        public int MaxTurns { get; set; }

        [JsonProperty("Heroes")]
        public List<HeroDTO> Heroes { get; set; }

        [JsonProperty("Board")]
        public BoardDTO BoardDto { get; set; }
        public bool Finished { get; set; }
    }
}