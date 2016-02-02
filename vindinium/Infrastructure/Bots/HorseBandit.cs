using System;
using System.Linq;

using vindinium.Infrastructure.Behaviors.Models;
using vindinium.Infrastructure.Behaviors.Movement;
using vindinium.Infrastructure.Behaviors.Tactics;

namespace vindinium.Infrastructure.Bots
{
    public class HorseBandit : IBot
    {
        private readonly Server server;

        public HorseBandit(Server server)
        {
            this.server = server;
        }

        public string BotName => "HorseBandit";

        //starts everything
        public void Run()
        {
            while (this.server.Finished == false && this.server.Errored == false)
            {
                var game = new Map(this.server.Board);
                var movement = new DefaultMovement(game);

                var tactics = new DumbGoldRush(game);
                var desination = tactics.NextDestination();

      
                
                var route = movement.GetShortestCompleteRouteToLocation(desination);

                var direction  = this.server.GetDirection(game.HeroNode.Location, route.First().Location);
                this.server.MoveHero(direction);
                Console.Out.WriteLine("completed turn " + this.server.CurrentTurn);

                //this.VisualizeMap(movement.Map);
                //Console.Out.WriteLine("Hero at : {0} {1} ", game.HeroCurrentLocation.X, game.HeroCurrentLocation.Y);
                //Console.Out.WriteLine("chest at : {0} {1} ", closestChest.location.X, closestChest.location.Y);
                //Console.Out.WriteLine("move to : {0} {1} ", route.First().location.X, route.First().location.Y);
            }

            if (this.server.Errored)
            {
                Console.Out.WriteLine("error: " + this.server.ErrorText);
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