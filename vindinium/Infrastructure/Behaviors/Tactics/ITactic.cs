using vindinium.Infrastructure.Behaviors.Map;
using vindinium.Infrastructure.Behaviors.Models;

namespace vindinium.Infrastructure.Behaviors.Tactics
{
    public interface ITactic
    {
        IMapNode NextDestination();
    }
}