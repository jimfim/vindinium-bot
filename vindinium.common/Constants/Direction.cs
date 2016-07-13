namespace vindinium.common.Entities
{
    public class Direction
    {
        public virtual int Id { get; protected set; }

        public const string Stay = "Stay";
        public const string North = "North";
        public const string East = "East";
        public const string South = "South";
        public const string West = "West";
    }
}