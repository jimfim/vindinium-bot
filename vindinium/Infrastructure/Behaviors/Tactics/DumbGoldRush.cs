using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;

using vindinium.Infrastructure.Behaviors.Models;
using vindinium.Infrastructure.DTOs;

namespace vindinium.Infrastructure.Behaviors.Tactics
{
    public class DumbGoldRush : ITactic
    {
        private readonly Map game;

        public DumbGoldRush(Map game)
        {
            this.game = game;
        }

        public CoOrdinates NextDestination()
        {
            return GetClosestChest().Location;
        }

        public Node GetClosestChest()
        {
            var viableChests = new List<Node>();
            for (int i = 0; i < game.NodeMap.GetLength(0); i++)
            {
                for (int j = 0; j < game.NodeMap.GetLength(1); j++)
                {
                    if (game.NodeMap[i, j].Type == Tile.GOLD_MINE_NEUTRAL || 
                        game.NodeMap[i, j].Type == Tile.GOLD_MINE_2 || 
                        game.NodeMap[i, j].Type == Tile.GOLD_MINE_3 || 
                        game.NodeMap[i, j].Type == Tile.GOLD_MINE_4)
                    {
                        viableChests.Add(game.NodeMap[i, j]);
                    }
                }
            }
            var closest = viableChests.OrderBy(c => c.MovementCost).First();
            return closest;
        }
    }

    public interface ITactic
    {
    }
}
