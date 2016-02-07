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
        /// <param name="server"></param>
        /// <returns></returns>
        public static IMapNode GetClosestChest(this Server server)
        {
            var tileset = new List<Tile> {Tile.GOLD_MINE_2, Tile.GOLD_MINE_3, Tile.GOLD_MINE_4, Tile.GOLD_MINE_NEUTRAL};
            var viableChests = Find(server, tileset);
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
        /// <param name="server"></param>
        /// <returns></returns>
        public static IMapNode GetClosestTavern(this Server server)
        {
            IMapNode closest = null;
            var viableTaverns = Find(server,Tile.TAVERN);
            if (viableTaverns.Any())
            {
                closest = viableTaverns.OrderBy(c => c.MovementCost).First();
            }
            return closest;
        }

        private static List<IMapNode> Find(Server server,Tile tile)
        {
            var tileset = new List<Tile> {tile};
            return Find(server, tileset);
        }

        private static List<IMapNode> Find(Server server, List<Tile> tileset)
        {
            var viableTargets = new List<IMapNode>();
            for (int y = 0; y < server.Board.Length; y++)
            {
                for (int x = 0; x < server.Board.Length; x++)
                {
                    viableTargets.AddRange(from tile in tileset where server.Board[x][y].Type == tile select server.Board[x][y]);
                }
            }
            return viableTargets;
            
        }
    }
}
