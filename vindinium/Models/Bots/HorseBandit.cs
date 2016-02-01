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
            Console.Out.WriteLine("random bot running");
            while (this.serverStuff.Finished == false && this.serverStuff.Errored == false)
            {
                Map board = new Map(this.serverStuff.Board);
                var moveBehavior = new DefaultMovement(board);
                
                var closestChest = moveBehavior.GetClosestChest(moveBehavior.HeroLocation());
                board.CalculateMovementCostFor(closestChest.location);
                var route = moveBehavior.GetShortestCompleteRouteToLocation(closestChest.location);
                this.serverStuff.MoveHero(moveBehavior.MoveToClosestChest(moveBehavior.HeroLocation(), route.First().location));
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