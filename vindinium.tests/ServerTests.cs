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
		 //   Server server = new Server("",true,50,"http://vindinium.org/","m4");
			//server.CreateBoard(size, map);
			//Assert.IsNotNull(server.Board);
	    }

        [Test]
        public void GetRoutes()
        {
            //Server server = new Server();
            //server.CreateBoard(size, map);
            //DefaultMapBuilder board = new DefaultMapBuilder(server);
            //var movement = new ShortestPath(board);
            //var closestChest = movement.GetClosestChest();

            //var route = movement.GetShortestCompleteRouteToLocation(closestChest.Location);

            //foreach (MapNode MapNode in route)
            //{
            //    Console.WriteLine("{0}-{1}",MapNode.Location.X,MapNode.Location.Y);
            //}

            //this.VisualizeMap(board);
            //Assert.IsNotNull(route);
        }

        //private void VisualizeMap(DefaultMapBuilder server)
        //{
        //    for (int i = 0; i < server.MapNodeMap.GetLength(0); i ++)
        //    {
        //        for (int j = 0; j < server.MapNodeMap.GetLength(1); j++)
        //        {

        //            Console.Write("{0}\t", server.MapNodeMap[i, j].MovementCost == int.MaxValue ? "#" : server.MapNodeMap[i, j].MovementCost.ToString());
        //        }
        //        Console.WriteLine();
        //    }
        //}


	    private void HeroMappingtest()
	    {
     //       Server server = new Server();
     //       server.CreateBoard(size, map);

        }
	}
}
