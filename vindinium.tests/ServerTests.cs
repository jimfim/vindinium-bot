using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;

using vindinium.Infrastructure.Behaviors.Models;
using vindinium.Infrastructure.Behaviors.Movement;

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
		    Server server = new Server("",true,50,"http://vindinium.org/","m4");
			server.CreateBoard(size, map);
			Assert.IsNotNull(server.Board);
	    }

        [Test]
        public void GetRoutes()
        {
            Server server = new Server();
            server.CreateBoard(size, map);
            DefaultMapBuilder board = new DefaultMapBuilder(server);
            var movement = new DefaultMovement(board);
            //var closestChest = movement.GetClosestChest();

            //var route = movement.GetShortestCompleteRouteToLocation(closestChest.Location);

            //foreach (Node node in route)
            //{
            //    Console.WriteLine("{0}-{1}",node.Location.X,node.Location.Y);
            //}

            this.VisualizeMap(board);
            //Assert.IsNotNull(route);
        }

        private void VisualizeMap(DefaultMapBuilder server)
        {
            for (int i = 0; i < server.NodeMap.GetLength(0); i ++)
            {
                for (int j = 0; j < server.NodeMap.GetLength(1); j++)
                {

                    Console.Write("{0}\t", server.NodeMap[i, j].MovementCost == int.MaxValue ? "#" : server.NodeMap[i, j].MovementCost.ToString());
                }
                Console.WriteLine();
            }
        }
	}
}
