using System;
using System.Threading;

using vindinium.Models.DTOs;

namespace vindinium.Models.Bots
{
    class RandomBot : IBot
    {
		public string BotName
		{
			get
			{
				return "RandomBot";
			}
		}

        private readonly ServerStuff serverStuff;

        public RandomBot(ServerStuff serverStuff)
        {
            this.serverStuff = serverStuff;
        }

        //starts everything
        public void Run()
        {
            Console.Out.WriteLine("random bot running");

            this.serverStuff.CreateGame();

            if (this.serverStuff.Errored == false)
            {
                //opens up a webpage so you can view the game, doing it async so we dont time out
                new Thread(delegate()
                {
                    System.Diagnostics.Process.Start(this.serverStuff.ViewUrl);
                }).Start();
            }
            
            Random random = new Random();
            while (this.serverStuff.Finished == false && this.serverStuff.Errored == false)
            {
                switch(random.Next(0, 6))
                {
                    case 0:
                        this.serverStuff.MoveHero(Direction.East);
                        break;
                    case 1:
                        this.serverStuff.MoveHero(Direction.North);
                        break;
                    case 2:
                        this.serverStuff.MoveHero(Direction.South);
                        break;
                    case 3:
                        this.serverStuff.MoveHero(Direction.Stay);
                        break;
                    case 4:
                        this.serverStuff.MoveHero(Direction.West);
                        break;
                }

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
