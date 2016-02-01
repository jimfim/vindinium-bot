using System;
using System.Linq;

using vindinium.Models.Behaviors;
using vindinium.Models.Behaviors.AStar;
using vindinium.Models.DTOs;

namespace vindinium.Models.Bots
{
	public class HorseBandit : IBot
	{
		public string BotName {
			get
			{
				return "HorseBandit";
			}
		}


		private readonly ServerStuff serverStuff;

		public HorseBandit(ServerStuff serverStuff)
		{
			this.serverStuff = serverStuff;
		}

		//starts everything
		public void Run()
		{
            while (this.serverStuff.Finished == false && this.serverStuff.Errored == false)
            {
                Map board = new Map(this.serverStuff.Board);
                var movement = new DefaultMovement(board);
                var herolocation = movement.HeroLocation();
                board.CalculateMovementCostFor(herolocation);
                var closestChest = movement.GetClosestChest(herolocation);

                Console.Out.WriteLine("Hero at : {0} {1} ", herolocation.X, herolocation.Y);
                Console.Out.WriteLine("chest at : {0} {1} ", closestChest.location.X, closestChest.location.Y);
                

                var route = movement.GetShortestCompleteRouteToLocation(closestChest.location);
                Console.Out.WriteLine("move to : {0} {1} ", route.First().location.X, route.First().location.Y);
                // var route = moveBehavior.GetShortestCompleteRouteToLocation(closestChest.location);

                this.serverStuff.MoveHero(movement.MoveToClosestChest(herolocation, route.First().location));
                Console.Out.WriteLine("completed turn " + this.serverStuff.CurrentTurn);
            }

            if (this.serverStuff.Errored)
            {
                Console.Out.WriteLine("error: " + this.serverStuff.ErrorText);
            }

            Console.Out.WriteLine("random bot Finished");
        }
	}
}