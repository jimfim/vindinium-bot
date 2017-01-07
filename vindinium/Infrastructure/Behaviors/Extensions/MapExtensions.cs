using System;
using System.Collections.Generic;
using System.Dynamic;
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

        public static List<Tile> NotMyTreasure(this Server server)
        {
            var tileset = new List<Tile> { Tile.GOLD_MINE_1, Tile.GOLD_MINE_2, Tile.GOLD_MINE_3, Tile.GOLD_MINE_4, Tile.GOLD_MINE_NEUTRAL };
            var mytreasure = MyTreasure(server);
            tileset.Remove(mytreasure);
            return tileset;
        }


        public static List<Tile> NotMe(this Server server)
        {
            var tileset = new List<Tile> { Tile.HERO_1, Tile.HERO_2, Tile.HERO_3, Tile.HERO_4};
            tileset.Remove(server.MyHero.Type);
            return tileset;
        }



        public static Tile MyTreasure(this Server server)
        {
            switch (server.MyHero.Type)
            {
                case Tile.HERO_1:
                    return Tile.GOLD_MINE_1;
                case Tile.HERO_2:
                    return Tile.GOLD_MINE_2;
                case Tile.HERO_3:
                    return Tile.GOLD_MINE_3;
                case Tile.HERO_4:
                    return Tile.GOLD_MINE_4;
                default:
                    return Tile.GOLD_MINE_1;
            }
        }
        /// <summary>
        /// returns closest *accessible* gold mine,and null if none available
        /// </summary>
        /// <param name="server"></param>
        /// <returns></returns>
        public static IMapNode GetClosestChest(this Server server)
        {
            var viableChests = Find(server, server.NotMyTreasure());
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

        public static IMapNode GetClosestEnemy(this Server server)
        {
            IMapNode closest = null;
            var closestEnemy = Find(server, server.NotMe());
            if (closestEnemy.Any())
            {
                closest = closestEnemy.OrderBy(c => c.MovementCost).First();
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
