using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vindinium.Infrastructure.Behaviors.Models;
using vindinium.Infrastructure.DTOs;

namespace vindinium.Infrastructure.Behaviors.Extensions
{
    public static class MapExtensions
    {
        /// <summary>
        /// returns closest *accessible* gold mine,and null if none available
        /// </summary>
        /// <param name="defaultMapBuilder"></param>
        /// <returns></returns>
        public static Node GetClosestChest(this IMapBuilder defaultMapBuilder)
        {
            var viableChests = new List<Node>();
            for (int i = 0; i < defaultMapBuilder.NodeMap.GetLength(0); i++)
            {
                for (int j = 0; j < defaultMapBuilder.NodeMap.GetLength(1); j++)
                {
                    if (defaultMapBuilder.NodeMap[i, j].Type == Tile.GOLD_MINE_NEUTRAL ||
                        defaultMapBuilder.NodeMap[i, j].Type == Tile.GOLD_MINE_2 ||
                        defaultMapBuilder.NodeMap[i, j].Type == Tile.GOLD_MINE_3 ||
                        defaultMapBuilder.NodeMap[i, j].Type == Tile.GOLD_MINE_4)
                    {
                        viableChests.Add(defaultMapBuilder.NodeMap[i, j]);
                    }
                }
            }
            Node closest = null;
            if (viableChests.Any())
            {
                closest = viableChests.OrderBy(c => c.MovementCost).First();
            }
            return closest;
        }

        /// <summary>
        /// returns closest *accessible* gold mine,and null if none available
        /// </summary>
        /// <param name="defaultMapBuilder"></param>
        /// <returns></returns>
        public static Node GetClosestTavern(this IMapBuilder defaultMapBuilder)
        {
            var viableTaverns = new List<Node>();
            for (int i = 0; i < defaultMapBuilder.NodeMap.GetLength(0); i++)
            {
                for (int j = 0; j < defaultMapBuilder.NodeMap.GetLength(1); j++)
                {
                    if (defaultMapBuilder.NodeMap[i, j].Type == Tile.TAVERN)
                    {
                        viableTaverns.Add(defaultMapBuilder.NodeMap[i, j]);
                    }
                }
            }
            Node closest = null;
            if (viableTaverns.Any())
            {
                closest = viableTaverns.OrderBy(c => c.MovementCost).First();
            }
            return closest;
        }
    }
}
