using vindinium.Infrastructure.DTOs;

namespace vindinium.Infrastructure.Behaviors.Models
{
    public interface IMapBuilder
    {
        Hero MyHero { get; }
        Node HeroNode { get; }
        Node[,] NodeMap { get; set; }
        
    }
}