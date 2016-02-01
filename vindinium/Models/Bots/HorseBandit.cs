using System;

using vindinium.Models.Behaviors;
using vindinium.Models.DTOs;

namespace vindinium.Models.Bots
{
	internal class HorseBandit : IBot
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
				var moveBehavior = new DefaultMovement(this.serverStuff.Board);
				this.serverStuff.MoveHero(moveBehavior.ToClosestChest());
				Console.Out.WriteLine("completed turn " + this.serverStuff.CurrentTurn);
			}

			if (this.serverStuff.Errored)
			{
				Console.Out.WriteLine("error: " + this.serverStuff.ErrorText);
			}

			Console.Out.WriteLine("random bot Finished");
		}

		private void MoveToClosestChest(Tile[][] board)
		{
			throw new NotImplementedException();
		}
	}
}