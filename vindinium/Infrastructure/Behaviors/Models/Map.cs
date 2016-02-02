using System.Collections.Generic;

using vindinium.Infrastructure.DTOs;

namespace vindinium.Infrastructure.Behaviors.Models
{
	public class Map
	{
		public int X { get; private set; }
		public int Y { get; private set; }
	    public Node HeroNode => this.GetHeroNode();

	    public Node[,] NodeMap { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="T:System.Object"/> class.
		/// </summary>
		public Map(Tile[,] gameBoard)
		{
			this.X = gameBoard.GetLength(0);
			this.Y = gameBoard.GetLength(1);
			this.NodeMap = this.ConvertToNodeMap(gameBoard);
			this.PopulateNodeParents();
		}

		private Node[,] ConvertToNodeMap(Tile[,] board)
		{
			Node[,] nodemap = new Node[board.GetLength(0),board.GetLength(1)];
		    int count = 0;
			for (int xIndex = 0; xIndex < board.GetLength(0); xIndex++)
			{
				for (int yIndex = 0; yIndex < board.GetLength(1); yIndex++)
				{
				    nodemap[xIndex, yIndex] = new Node(board[xIndex, yIndex], xIndex, yIndex) { Id = count };
				    count++;
				}
			}
			return nodemap;
		}

		public void PopulateNodeParents()
		{
			for (int i = 0; i < this.X; i++)
			{
				for (int j = 0; j < this.Y; j++)
				{
					var node = this.NodeMap[i,j];
					var parents = this.GetParents(this.NodeMap[i, j]);
					node.Parents = parents;
				}
			}
		}

        private Node GetHeroNode()
        {
            Node heroNode = null;
            for (int i = 0; i < this.NodeMap.GetLength(0); i++)
            {
                for (int j = 0; j < this.NodeMap.GetLength(1); j++)
                {
                    if (this.NodeMap[i, j].Type == Tile.HERO_1)
                    {
                        heroNode = this.NodeMap[i, j];
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
				var north = this.NodeMap[sourceNode.Location.X,sourceNode.Location.Y - 1];
				results.Add(north);
			}

			if (sourceNode.Location.Y + 1 <= this.NodeMap.GetLength(0) - 1)
			{
				var south = this.NodeMap[sourceNode.Location.X,sourceNode.Location.Y + 1];
				results.Add(south);
			}

			if (sourceNode.Location.X + 1 <= this.NodeMap.GetLength(1) - 1)
			{
				var east = this.NodeMap[sourceNode.Location.X + 1,sourceNode.Location.Y];
		        results.Add(east);
			}

			if (sourceNode.Location.X - 1 >= 0)
			{
				var west = this.NodeMap[sourceNode.Location.X - 1,sourceNode.Location.Y];
				results.Add(west);
			}
			return results;
		}
	}
}