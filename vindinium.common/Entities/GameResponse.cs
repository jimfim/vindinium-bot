namespace vindinium.common.Entities
{
    public class GameResponse
    {
        public virtual int Id { get; protected set; }

        public virtual Game Game { get; protected set; }

        public virtual Hero Hero { get; protected set; }

        public virtual string PlayUrl { get; protected set; }

        public virtual string Token { get; protected set; }

        public virtual string ViewUrl { get; protected set; }

    }
}