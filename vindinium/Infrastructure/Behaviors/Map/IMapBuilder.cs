using vindinium.Infrastructure.Behaviors.Models;

namespace vindinium.Infrastructure.Behaviors.Map
{
    public interface IMapBuilder
    {
        IMapNode MyHero { get; }
        IMapNode[,] MapNodeMap { get; set; }
        
    }
}