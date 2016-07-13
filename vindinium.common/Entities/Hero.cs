namespace vindinium.common.Entities
{
    public class Hero
    {
        public virtual bool Crashed { get; protected set; }

        public virtual int Elo { get; protected set; }

        public virtual int Gold { get; protected set; }

        public virtual int Id { get; protected set; }

        public virtual int Life { get; protected set; }

        public virtual int MineCount { get; protected set; }

        public virtual string Name { get; protected set; }

        public virtual Pos Pos { get; protected set; }

        public virtual Pos SpawnPos { get; protected set; }
    }
}