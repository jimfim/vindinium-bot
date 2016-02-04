using System.Collections.Generic;

using vindinium.Infrastructure.Behaviors.Map;
using vindinium.Infrastructure.DTOs;

namespace vindinium.Infrastructure.Behaviors.Models
{
    public class DefaultMapBuilder : IMapBuilder
    {
        private readonly Server server;
        public IMapNode MyHero => this.server.MyHero;

        public IMapNode[,] MapNodeMap { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public DefaultMapBuilder(Server server)
        {
            this.server = server;
            PopulateNodeParents();
        }

        private void PopulateNodeParents()
        {
            for (int i = 0; i < this.MapNodeMap.GetLength(0); i++)
            {
                for (int j = 0; j < this.MapNodeMap.GetLength(1); j++)
                {
                    var node = this.MapNodeMap[i, j];
                    var parents = GetParents(this.MapNodeMap[i, j]);
                    node.Parents = parents;
                }
            }
        }

        private List<IMapNode> GetParents(IMapNode sourceMapNode)
        {
            List<IMapNode> results = new List<IMapNode>();
            if (sourceMapNode.Location.Y - 1 >= 0)
            {
                var north = this.MapNodeMap[sourceMapNode.Location.X, sourceMapNode.Location.Y - 1];
                results.Add(north);
            }

            if (sourceMapNode.Location.Y + 1 <= this.MapNodeMap.GetLength(0) - 1)
            {
                var south = this.MapNodeMap[sourceMapNode.Location.X, sourceMapNode.Location.Y + 1];
                results.Add(south);
            }

            if (sourceMapNode.Location.X + 1 <= this.MapNodeMap.GetLength(1) - 1)
            {
                var east = this.MapNodeMap[sourceMapNode.Location.X + 1, sourceMapNode.Location.Y];
                results.Add(east);
            }

            if (sourceMapNode.Location.X - 1 >= 0)
            {
                var west = this.MapNodeMap[sourceMapNode.Location.X - 1, sourceMapNode.Location.Y];
                results.Add(west);
            }
            return results;
        }
    }
}