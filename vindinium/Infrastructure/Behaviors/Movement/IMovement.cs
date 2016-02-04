using System.Collections.Generic;

using vindinium.Infrastructure.Behaviors.Map;
using vindinium.Infrastructure.Behaviors.Models;

namespace vindinium.Infrastructure.Behaviors.Movement
{
	public interface IMovement
	{
        List<IMapNode> GetShortestCompleteRouteToLocation(CoOrdinates closestChest);
    }
}