using System;
using System.Threading;

using vindinium.Infrastructure.DTOs;

namespace vindinium.Infrastructure.Bots
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

        private readonly Server server;

        public RandomBot(Server server)
        {
            this.server = server;
        }

        //starts everything
        public void Run()
        {
            Console.Out.WriteLine("random bot running");

            this.server.CreateGame();

            if (this.server.Errored == false)
            {
                //opens up a webpage so you can view the game, doing it async so we dont time out
                new Thread(delegate()
                {
                    System.Diagnostics.Process.Start(this.server.ViewUrl);
                }).Start();
            }
            
            Random random = new Random();
            while (this.server.Finished == false && this.server.Errored == false)
            {
                switch(random.Next(0, 6))
                {
                    case 0:
                        this.server.MoveHero(Direction.East);
                        break;
                    case 1:
                        this.server.MoveHero(Direction.North);
                        break;
                    case 2:
                        this.server.MoveHero(Direction.South);
                        break;
                    case 3:
                        this.server.MoveHero(Direction.Stay);
                        break;
                    case 4:
                        this.server.MoveHero(Direction.West);
                        break;
                }

                Console.Out.WriteLine("completed turn " + this.server.CurrentTurn);
            }

            if (this.server.Errored)
            {
                Console.Out.WriteLine("error: " + this.server.ErrorText);
            }

            Console.Out.WriteLine("random bot Finished");
        }


    }
}
