using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using vindinium.Infrastructure.Behaviors.Map;
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
        public static IMapNode GetClosestChest(this Server server)
        {
            var viableChests = new List<IMapNode>();
            for (int i = 0; i < server.Board.GetLength(0); i++)
            {
                for (int j = 0; j < server.Board.GetLength(1); j++)
                {
                    if (server.Board[i, j].Type == Tile.GOLD_MINE_NEUTRAL ||
                        server.Board[i, j].Type == Tile.GOLD_MINE_2 ||
                        server.Board[i, j].Type == Tile.GOLD_MINE_3 ||
                        server.Board[i, j].Type == Tile.GOLD_MINE_4)
                    {
                        viableChests.Add(server.Board[i, j]);
                    }
                }
            }
            IMapNode closest = null;
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
        public static IMapNode GetClosestTavern(this Server server)
        {
            var viableTaverns = new List<IMapNode>();
            for (int i = 0; i < server.Board.GetLength(0); i++)
            {
                for (int j = 0; j < server.Board.GetLength(1); j++)
                {
                    if (server.Board[i, j].Type == Tile.TAVERN)
                    {
                        viableTaverns.Add(server.Board[i, j]);
                    }
                }
            }
            IMapNode closest = null;
            if (viableTaverns.Any())
            {
                closest = viableTaverns.OrderBy(c => c.MovementCost).First();
            }
            return closest;
        }
    }
}
