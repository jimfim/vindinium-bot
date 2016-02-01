using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;

using vindinium.Models.Behaviors;
using vindinium.Models.Behaviors.AStar;
using vindinium.Models.Bots;
using vindinium.Models.DTOs;

namespace vindinium.tests
{
	[TestFixture]
    public class ServerTests
	{
		int size = 12;
		string map = @"################################        ############$-            $-########  @1        @4  ######    []  $-$-  []    ####    ##  ####  ##    ####    ##  ####  ##    ####    []  $-$-  []    ######  @2        @3  ########$-            $-############        ################################";

	    [Test]
	    public void ParseMap()
	    {
		    ServerStuff server = new ServerStuff("s",true,50,"http://vindinium.org/","m2");
			server.CreateBoard(size, map);
			Assert.IsNotNull(server.Board);
	    }

        [Test]
        public void GetRoutes()
        {
            ServerStuff server = new ServerStuff("s", true, 50, "http://vindinium.org/", "m2");
            server.CreateBoard(size, map);
            Map board = new Map(server.Board);

            var heroLocation = new CoOrdinates(2, 3);
            board.CalculateMovementCostFor(heroLocation);

            var movement = new DefaultMovement(board);
            var closestChest = movement.GetClosestChest(heroLocation);

            var route = movement.GetShortestCompleteRouteToLocation(closestChest.location);
            this.VisualizeMap(board);

            Console.WriteLine("move order");
            foreach (var node in route)
            {
                Console.WriteLine("{0}-{1}", node.location.X,node.location.Y);
            }

            Assert.IsNotNull(route);
        }

        private void VisualizeMap(Map server)
        {
            for (int i = 0; i < server.NodeMap.GetLength(0); i ++)
            {
                for (int j = 0; j < server.NodeMap.GetLength(1); j++)
                {
                    if (!server.NodeMap[i, j].Passable)
                    {
                        Console.Write("{0}|{1}:{2}\t", "#","#","#");
                    }
                    else
                    {
                        Console.Write("{0}|{1}:{2}\t", server.NodeMap[i, j].H, server.NodeMap[i, j].location.X, server.NodeMap[i, j].location.Y);
                    }
                }
                Console.WriteLine();
            }
        }
	}
}
