namespace vindinium.common.Entities
{
    public class GameDetails
    {
        public virtual int Id { get; set; }

        public virtual string Reference { get; set; }

        public virtual Round Round { get; set; }

        public virtual Hero Hero { get; set; }

        public virtual string PlayUrl { get; set; }

        public virtual string Token { get; set; }

        public virtual string ViewUrl { get; set; }

    }
}