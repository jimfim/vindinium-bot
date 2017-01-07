using Newtonsoft.Json;

namespace vindinium.common.Dtos
{
    public class HeroDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }
        public int Elo { get; set; }
        [JsonProperty("Pos")]
        public PosDTO PosDto { get; set; }
        public string LastDir { get; set; }
        public int Life { get; set; }
        public int Gold { get; set; }
        public int MineCount { get; set; }
        [JsonProperty("SpawnPos")]
        public SpawnPosDTO SpawnPosDto { get; set; }
        public bool Crashed { get; set; }
    }
}