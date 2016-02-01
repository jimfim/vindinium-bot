using System;
using System.Linq;
using vindinium.Models.Behaviors;
using vindinium.Models.Behaviors.AStar;

namespace vindinium.Models.Bots
{
    public class HorseBandit : IBot
    {
        private readonly ServerStuff serverStuff;

        public HorseBandit(ServerStuff serverStuff)
        {
            this.serverStuff = serverStuff;
        }

        public string BotName
        {
            get { return "HorseBandit"; }
        }

        //starts everything
        public void Run()
        {
            while (serverStuff.Finished == false && serverStuff.Errored == false)
            {
                var board = new Map(serverStuff.Board);
                var movement = new DefaultMovement(board);

                var closestChest = movement.GetClosestChest();
                Console.Out.WriteLine("Hero at : {0} {1} ", board.HeroCurrentLocation.X, board.HeroCurrentLocation.Y);
                Console.Out.WriteLine("chest at : {0} {1} ", closestChest.location.X, closestChest.location.Y);
                //Console.Out.WriteLine("move to : {0} {1} ", route.First().location.X, route.First().location.Y);
                var route = movement.GetShortestCompleteRouteToLocation(closestChest.location);

                var direction  = serverStuff.GetDirection(board.HeroCurrentLocation, route.First().location);
                serverStuff.MoveHero(direction);
                Console.Out.WriteLine("completed turn " + serverStuff.CurrentTurn);

                VisualizeMap(movement.Map);
            }

            if (serverStuff.Errored)
            {
                Console.Out.WriteLine("error: " + serverStuff.ErrorText);
            }

            Console.Out.WriteLine("random bot Finished");
        }


        private void VisualizeMap(Map server)
        {

            for (int i = 0; i < server.NodeMap.GetLength(0); i++)
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