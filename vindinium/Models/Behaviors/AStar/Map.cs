using System.Collections.Generic;

using vindinium.Models.DTOs;

namespace vindinium.Models.Behaviors.AStar
{
	public class Map
	{
		public int X { get; private set; }
		public int Y { get; private set; }

		public Node[,] NodeMap { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="T:System.Object"/> class.
		/// </summary>
		public Map(Tile[,] gameBoard)
		{
			X = gameBoard.GetLength(0);
			Y = gameBoard.GetLength(1);
			this.NodeMap = ConvertToNodeMap(gameBoard);
			this.CalculateParents();
		}

		private Node[,] ConvertToNodeMap(Tile[,] board)
		{
			//var width = board.GetLength(0);
			//var length = board.GetLength(1);
			Node[,] nodemap = new Node[board.GetLength(0),board.GetLength(1)];
		    int count = 0;
			for (int xIndex = 0; xIndex < board.GetLength(0); xIndex++)
			{
				for (int yIndex = 0; yIndex < board.GetLength(1); yIndex++)
				{
					nodemap[xIndex,yIndex] = new Node(board[xIndex,yIndex], xIndex, yIndex);
				    nodemap[xIndex, yIndex].Id = count;
				    count++;
                    if (board[xIndex, yIndex] == Tile.FREE || board[xIndex, yIndex] == Tile.GOLD_MINE_2 || board[xIndex, yIndex] == Tile.GOLD_MINE_3 || board[xIndex, yIndex] == Tile.GOLD_MINE_4 || board[xIndex, yIndex] == Tile.GOLD_MINE_NEUTRAL)
                    {
						nodemap[xIndex, yIndex].Passable = true;
					}
				}
			}
			return nodemap;
		}

		public void CalculateParents()
		{
			for (int i = 0; i < this.X; i++)
			{
				for (int j = 0; j < this.Y; j++)
				{
					var node = this.NodeMap[i,j];
					var parents = this.GetParents(i, j);
					node.Parents = parents;
				}
			}
		}

		//public void CalculateGForStart(int x, int y)
		//{
		//	var node = this.NodeMap[x,y];
		//	node.CalculateG();
		//}

		public void CalculateMovementCostFor(CoOrdinates coOrdinates)
		{
			for (int i = 0; i < this.X; i++)
			{
				for (int j = 0; j < this.Y; j++)
				{
					var node = this.NodeMap[i,j];
					node.H = node.CalculateH(coOrdinates.X, coOrdinates.Y);
				}
			}

			this.NodeMap[coOrdinates.X, coOrdinates.Y].Parents = null;
			this.NodeMap[coOrdinates.X, coOrdinates.Y].H = 0;
		}

		private List<Node> GetParents(int x, int y)
		{
			return this.GetParents(this.NodeMap[x,y]);
		}

		private List<Node> GetParents(Node sourceNode)
		{

			List<Node> results = new List<Node>();
			if (sourceNode.location.Y - 1 >= 0)
			{
				var north = this.NodeMap[sourceNode.location.X,sourceNode.location.Y - 1];
				if (north.Passable)
				{
					results.Add(north);
				}
			}

			if (sourceNode.location.Y + 1 <= this.NodeMap.GetLength(0) - 1)
			{
				var south = this.NodeMap[sourceNode.location.X,sourceNode.location.Y + 1];
				if (south.Passable)
				{
					results.Add(south);
				}
			}

			if (sourceNode.location.X + 1 <= this.NodeMap.GetLength(1) - 1)
			{
				var east = this.NodeMap[sourceNode.location.X + 1,sourceNode.location.Y];
				if (east.Passable)
				{
					results.Add(east);
				}
			}

			if (sourceNode.location.X - 1 >= 0)
			{
				var west = this.NodeMap[sourceNode.location.X - 1,sourceNode.location.Y];
				if (west.Passable)
				{
					results.Add(west);
				}
			}
			return results;
		}
	}
}