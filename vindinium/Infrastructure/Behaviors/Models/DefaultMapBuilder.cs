using System.Collections.Generic;
using vindinium.Infrastructure.DTOs;

namespace vindinium.Infrastructure.Behaviors.Models
{
    public class DefaultMapBuilder : IMapBuilder
    {
        private readonly Server _server;
        public Hero MyHero => _server.MyHero;
        public Node HeroNode => GetHeroNode();

        public Node[,] NodeMap { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public DefaultMapBuilder(Server server)
        {
            _server = server;
            NodeMap = ConvertToNodeMap(server.Board);
            PopulateNodeParents();
        }

        private Node[,] ConvertToNodeMap(Tile[,] board)
        {
            Node[,] nodemap = new Node[board.GetLength(0), board.GetLength(1)];
            int count = 0;
            for (int xIndex = 0; xIndex < board.GetLength(0); xIndex++)
            {
                for (int yIndex = 0; yIndex < board.GetLength(1); yIndex++)
                {
                    nodemap[xIndex, yIndex] = new Node(board[xIndex, yIndex], xIndex, yIndex) {Id = count};
                    count++;
                }
            }
            return nodemap;
        }

        private void PopulateNodeParents()
        {
            for (int i = 0; i < NodeMap.GetLength(0); i++)
            {
                for (int j = 0; j < NodeMap.GetLength(1); j++)
                {
                    var node = NodeMap[i, j];
                    var parents = GetParents(NodeMap[i, j]);
                    node.Parents = parents;
                }
            }
        }

        private Node GetHeroNode()
        {
            Node heroNode = null;
            for (int i = 0; i < NodeMap.GetLength(0); i++)
            {
                for (int j = 0; j < NodeMap.GetLength(1); j++)
                {
                    if (NodeMap[i, j].Type == Tile.HERO_1)
                    {
                        heroNode = NodeMap[i, j];
                    }
                }
            }
            return heroNode;
        }

        private List<Node> GetParents(Node sourceNode)
        {
            List<Node> results = new List<Node>();
            if (sourceNode.Location.Y - 1 >= 0)
            {
                var north = NodeMap[sourceNode.Location.X, sourceNode.Location.Y - 1];
                results.Add(north);
            }

            if (sourceNode.Location.Y + 1 <= NodeMap.GetLength(0) - 1)
            {
                var south = NodeMap[sourceNode.Location.X, sourceNode.Location.Y + 1];
                results.Add(south);
            }

            if (sourceNode.Location.X + 1 <= NodeMap.GetLength(1) - 1)
            {
                var east = NodeMap[sourceNode.Location.X + 1, sourceNode.Location.Y];
                results.Add(east);
            }

            if (sourceNode.Location.X - 1 >= 0)
            {
                var west = NodeMap[sourceNode.Location.X - 1, sourceNode.Location.Y];
                results.Add(west);
            }
            return results;
        }
    }
}