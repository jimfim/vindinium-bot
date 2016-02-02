using vindinium.Infrastructure.Behaviors.Models;

namespace vindinium.Infrastructure.Behaviors.Tactics
{
    public interface ITactic
    {
        Node NextDestination();
    }
}