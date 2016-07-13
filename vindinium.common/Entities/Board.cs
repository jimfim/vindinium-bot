namespace vindinium.common.Entities
{
    public class Board
    {
        public virtual int Id { get; protected set; }

        public virtual int Size { get; protected set; }

        public virtual string Tiles { get; protected set; }
    }
}