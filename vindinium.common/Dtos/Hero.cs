namespace vindinium.common.Dtos
{
    public class Hero
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }
        public int Elo { get; set; }
        public Pos Pos { get; set; }
        public string LastDir { get; set; }
        public int Life { get; set; }
        public int Gold { get; set; }
        public int MineCount { get; set; }
        public SpawnPos SpawnPos { get; set; }
        public bool Crashed { get; set; }
    }
}