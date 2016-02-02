namespace vindinium.Infrastructure.Behaviors.Models
{
    public class CoOrdinates
    {
        public CoOrdinates(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
        public int X { get; set; }

        public int Y { get; set; }
    }
}