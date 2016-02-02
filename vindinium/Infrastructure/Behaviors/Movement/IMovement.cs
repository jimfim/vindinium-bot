using System.Collections.Generic;

using vindinium.Infrastructure.Behaviors.Models;

namespace vindinium.Infrastructure.Behaviors.Movement
{
	public interface IMovement
	{
        List<Node> GetShortestCompleteRouteToLocation(CoOrdinates closestChest);
    }
}