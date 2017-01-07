namespace vindinium.common.Entities
{
    public class Hero
    {
        public virtual bool Crashed { get; set; }

        public virtual int Elo { get; set; }

        public virtual int Gold { get; set; }

        public virtual int Id { get; set; }

        public virtual int Life { get; set; }

        public virtual int MineCount { get; set; }

        public virtual string Name { get; set; }

        public virtual Pos Pos { get; set; }

        public virtual Pos SpawnPos { get; set; }

        public virtual string LastDir { get; set; }

        public virtual string UserId { get; set; }
    }
}